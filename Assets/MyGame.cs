using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class MyGame : MonoBehaviour
{
    public GameObject[] colorObjects; // Массив объектов с цветами
    public int sequenceLength = 4; // Длина последовательности цветов
    public float lightUpDuration = 1.0f; // Продолжительность подсветки в секундах
    public float timeBetweenColors = 0.5f; // Время между подсветками в секундах
    private List<int> sequence = new List<int>(); // Диннамический контейнер для хранения последовательности подсветки
    private int currentIndex = 0; // Индекс текущего объекта для подсветки
    private bool playerInputEnabled;
    private bool isGameOver = false;
    public float updateInterval = 10.0f; // Интервал обновления в секундах


    private void Start()
    {
        StartCoroutine(UpdateRandomCubes());
        StartCoroutine(PlaySequence());
    }


    private void Update()
    {
        ClickSquare();
    }


    private void ClickSquare()
    {
        if (Input.GetMouseButtonDown(0) && !isGameOver)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject; 
                int index = System.Array.IndexOf(colorObjects, clickedObject); // записываем объект на который нажали
                if (CompareWithSequence(index))
                {
                    currentIndex++;
                    LightUpObject(index);
                    PlayAudioClick(index);
                    if (currentIndex == sequenceLength)
                    {
                        sequenceLength++;
                        Debug.Log("Вы Выиграли!");
                        currentIndex = 0;
                        StartCoroutine(PlaySequence());
                    }
                }
                else
                {
                    isGameOver = true;
                    Debug.Log("Вы проиграли!");
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
                Debug.Log(order);
                return true;
            } 
                

        }

        //for (int i = 0; i < sequenceLength; i++)
        //{
        //    if (index == sequence[i])
        //    {
        //        return true;
        //    }
        //}
        return false;
    }

    void RandomCubes()
    {
        //sequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add(UnityEngine.Random.Range(0, colorObjects.Length));
        }
    }

    void PlayAudioClick (int index)
    {
        colorObjects[index].GetComponent<AudioSource>().Play();
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

    IEnumerator DelayedTurnOff(int index, float delay) // Корутина для вызова метода TurnOffObject через задержку
    {
        yield return new WaitForSeconds(delay);
        TurnOffObject(index);
    }
    IEnumerator PlaySequence() // Подсветка объектов по порядку из массива sequence
    {
        foreach (int index in sequence)
        {
            LightUpObject(index);
            Debug.Log($"{index}");
            yield return new WaitForSeconds(lightUpDuration); // Продолжительность подсветки в секундах
            TurnOffObject(index);
            yield return new WaitForSeconds(timeBetweenColors);
            playerInputEnabled = true;
        }
    }
    IEnumerator UpdateRandomCubes()
    {
        yield return new WaitForSeconds(10f);

        while (true) // Бесконечный цикл, чтобы корутина выполнялась постоянно
        {
            RandomCubes();
            yield return new WaitForSeconds(updateInterval + 1f);
        }
    }
}
