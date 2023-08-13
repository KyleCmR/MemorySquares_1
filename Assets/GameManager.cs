using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] _colorObjects;
    private List<int> _sequence = new List<int>();
    private int _sequenceLength = 1;

    public float _lightUpDuration = 1.2f;
    public float _timeBetweenColors = 0.3f;
    private int _currentIndex = 0;

    private bool _playerInputEnabled = false;
    private bool _isGameOver = false;

    public GameObject _losePanel;
    public PlayAudio _playAudio;
    public Transform _transfomObj;
    private Scene _currentScene;

    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _correctSequence;
    [SerializeField] TextMeshProUGUI _status;

    private void Awake()
    {
        _playAudio = GetComponent<PlayAudio>();
        _currentScene = SceneManager.GetActiveScene();
        RandomCubes();
    }
    private void Start()
    {
        StartCoroutine(PlaySequence());
    }

    private void Update()
    {
        ClickSquare();
        CheckStatus();
        _correctSequence.text = _currentIndex.ToString() + "/" + _sequenceLength.ToString();
    }


    private void ClickSquare()
    {
        if (Input.GetMouseButtonDown(0) && !_isGameOver && _playerInputEnabled)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                int index = Array.IndexOf(_colorObjects, clickedObject);

                if (CompareWithSequence(index))
                {
                    _currentIndex++;
                    LightUp(index);
                    _playAudio.PlayClip(0);

                    if (_currentIndex == _sequenceLength)
                    {
                        _sequenceLength++;
                        _playAudio.PlayClip(2);
                        _currentIndex = 0;
                        RandomCubes();
                        StartCoroutine(PlaySequence());
                        _playerInputEnabled = false;
                        _score.text = _sequenceLength.ToString();
                    }
                }
                else
                {
                    _playAudio.PlayClip(1);
                    _losePanel.SetActive(true);
                    _isGameOver = true;
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
        return index == _sequence[_currentIndex] 
            ? true
            : false;
    }

    void RandomCubes()
    {
        if (_currentScene.buildIndex == 1)
        {
            _sequence.Add(UnityEngine.Random.Range(0, _colorObjects.Length));
        }
        else if (_currentScene.buildIndex == 2)
        {
            _sequence.Clear();
            for (int i = 0; i < _sequenceLength; i++)
            {
                _sequence.Add(UnityEngine.Random.Range(0, _colorObjects.Length));
            }
        }
    }


    void CheckStatus()
    {
        _status.text = !_playerInputEnabled 
            ? "Çàïîìèíàé" 
            : "Ïîâòîðè";

        _status.color = !_playerInputEnabled 
            ? Color.red 
            : Color.green;

        if (_isGameOver)
        {
            _status.text = "ÏÎÐÀÆÅÍÈÅ";
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

    IEnumerator LightDown1sec(int index, float delay) 
    {
        yield return new WaitForSeconds(delay);
        LightDown(index);
    }

    IEnumerator PlaySequence() 
    {
        yield return new WaitForSeconds(1f);
        foreach (int index in _sequence)
        {
            yield return new WaitForSeconds(0.2f);
            LightUp(index);
            _playAudio.PlayClip(3);
            yield return new WaitForSeconds(_lightUpDuration);
            LightDown(index);
            yield return new WaitForSeconds(_timeBetweenColors);
        }
        yield return _playerInputEnabled = true;
    }
}
