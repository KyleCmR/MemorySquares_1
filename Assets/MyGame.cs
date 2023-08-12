using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

public class MyGame : MonoBehaviour
{
    public GameObject[] _colorObjects; // ������ �������� � �������
    private List<int> _sequence = new List<int>(); // ������������� ��������� ��� �������� ������������������ ���������
    private int sequenceLength = 1; // ��������� ����� ������������������ ������

    public float lightUpDuration = 1.5f; // ����������������� ��������� � ��������
    public float timeBetweenColors = 0.5f; // ����� ����� ����������� � ��������
    private int currentIndex = 0; // ������ �������� ������� ��� ���������

    private bool playerInputEnabled = false;
    private bool isGameOver = false;

    public GameObject _losePanel;
    public PlayAudio _playAudio;
    public Transform _transfomObj;

    //�����
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _correctSequence;
    [SerializeField] TextMeshProUGUI _status;


    private void Awake()
    {
        _playAudio = GetComponent<PlayAudio>();
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
                int index = Array.IndexOf(_colorObjects, clickedObject);// ���������� ������ �� ������� ������

                if (CompareWithSequence(index))
                {
                    currentIndex++;
                    LightUp(index);
                    _playAudio.PlayClip(0);

                    if (currentIndex == sequenceLength)
                    {
                        sequenceLength++;
                        _playAudio.PlayClip(2);
                        currentIndex = 0;
                        RandomCubes();
                        StartCoroutine(PlaySequence());
                        playerInputEnabled = false;
                        _score.text = sequenceLength.ToString();
                    }
                }
                else
                {
                    _playAudio.PlayClip(1);
                    _losePanel.SetActive(true);
                    isGameOver = true;
                    foreach (GameObject obj in _colorObjects)
                    {
                        obj.SetActive(false);
                    }
                }

            }
        }
    }
    private bool CompareWithSequence(int index)
    {
        foreach (var order in _sequence)
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
        _sequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            _sequence.Add(UnityEngine.Random.Range(0, _colorObjects.Length));
        }
    }

    void CheckStatus()
    {
        _status.text = !playerInputEnabled 
            ? "���������" 
            : "�������";

        _status.color = !playerInputEnabled 
            ? Color.red 
            : Color.green;

        if (isGameOver)
        {
            _status.text = "������";
            _status.color = Color.red;
        }
    }

    void LightUp(int index)
    {
        _colorObjects[index].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        StartCoroutine(LightDown1sec(index, 0.5f));
    }

    void LightDown(int index)
    {
        _colorObjects[index].GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }

    IEnumerator LightDown1sec(int index, float delay) // �������� ��� ������ ������ LightDown ����� ��������
    {
        yield return new WaitForSeconds(delay);
        LightDown(index);
    }

    IEnumerator PlaySequence() // ��������� �������� �� ������� �� ������� sequence
    {
        yield return new WaitForSeconds(1f);
        foreach (int index in _sequence)
        {
            yield return new WaitForSeconds(0.5f);
            LightUp(index);
            _playAudio.PlayClip(3);
            yield return new WaitForSeconds(lightUpDuration); // ����������������� ��������� � ��������
            LightDown(index);
            yield return new WaitForSeconds(timeBetweenColors);
        }
        yield return playerInputEnabled = true;
    }
}
