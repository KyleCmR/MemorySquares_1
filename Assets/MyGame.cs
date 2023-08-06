using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

public class MyGame : MonoBehaviour
{
    public GameObject[] colorObjects; // ������ �������� � �������
    private int sequenceLength = 2; // ����� ������������������ ������
    public float lightUpDuration = 1.0f; // ����������������� ��������� � ��������
    public float timeBetweenColors = 0.5f; // ����� ����� ����������� � ��������
    private List<int> sequence = new List<int>(1); // ������������� ��������� ��� �������� ������������������ ���������
    private int currentIndex = 0; // ������ �������� ������� ��� ���������
    private bool playerInputEnabled = false;
    private bool isGameOver = false;
    public float updateInterval = 10.0f; // �������� ���������� � ��������
    public GameObject _losePanel;
    public AudioSource _audioSource;
    public PlayAudio _playAudio;
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _correctSequence;
    [SerializeField] TextMeshProUGUI _status;



    private void Awake()
    {
        _playAudio = GetComponent<PlayAudio>();
        _audioSource = GetComponent<AudioSource>();
        RandomCubes();

    }
    private void Start()
    {
        
        StartCoroutine(PlaySequence());
    }

    private void Update()
    {
        ClickSquare();
        _correctSequence.text = currentIndex.ToString() + "/" + sequenceLength.ToString();
        CheckStatus();
    }


    private void ClickSquare()
    {

        if (Input.GetMouseButtonDown(0) && !isGameOver && playerInputEnabled)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject; 
                int index = System.Array.IndexOf(colorObjects, clickedObject);// ���������� ������ �� ������� ������
                
                if (CompareWithSequence(index))
                {
                    currentIndex++;
                    LightUpObject(index);
                    _playAudio.PlayClip(4);
                    
                    if (currentIndex == sequenceLength)
                    {
                        sequenceLength++;
                        _playAudio.PlayClip(5);
                        currentIndex = 0;
                        RandomCubes();
                        StartCoroutine(PlaySequence());
                        playerInputEnabled = false;
                        _score.text = sequenceLength.ToString();
                    }
                }
                else
                {               
                    _playAudio.PlayClip(2);
                    _losePanel.SetActive(true);
                    isGameOver = true;
                    foreach (GameObject obj in colorObjects)
                    {
                        obj.SetActive(false);
                    }
                }

            }
        }
    }
    private bool CompareWithSequence(int index)
    {
        foreach (var order in sequence)
        {
            if (index == order)
            {
                return true;
            }    
        }
        return false;
    }

    void RandomCubes()
    {
        sequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add(UnityEngine.Random.Range(0, colorObjects.Length));
        }
    }

    void CheckStatus()
    {
        if (playerInputEnabled == false)
        {
            _status.text = "���������";
            _status.color = Color.red;
        }
        else
        {
            _status.text = "�������";
            _status.color = Color.green;
        }
        if (isGameOver)
        {
            _status.text = "������";
            _status.color = Color.red;
        }    
    }

    void LightUpObject(int index)
    {
        colorObjects[index].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        StartCoroutine(DelayedTurnOff(index, 1f));
    }

    void TurnOffObject(int index)
    {
        colorObjects[index].GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }

    IEnumerator DelayedTurnOff(int index, float delay) // �������� ��� ������ ������ TurnOffObject ����� ��������
    {
        yield return new WaitForSeconds(delay);
        TurnOffObject(index);
    }

    IEnumerator PlaySequence() // ��������� �������� �� ������� �� ������� sequence
    {
        foreach (int index in sequence)
        {
            yield return new WaitForSeconds(1.5f);
            LightUpObject(index);
            _playAudio.PlayClip(3);
            yield return new WaitForSeconds(lightUpDuration); // ����������������� ��������� � ��������
            TurnOffObject(index);
            yield return new WaitForSeconds(timeBetweenColors);     
        }
        yield return playerInputEnabled = true;
    }
}
