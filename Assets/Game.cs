//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class Game : MonoBehaviour
//{
//    public GameObject[] colorObjects; // Массив объектов с цветами
//    public int sequenceLength = 4; // Длина последовательности цветов
//    public float lightUpDuration = 1.0f; // Продолжительность подсветки в секундах
//    public float timeBetweenColors = 0.5f; // Время между подсветками в секундах

//    private int[] sequence; // Массив для хранения последовательности подсветки
//    private int currentIndex; // Индекс текущего объекта для подсветки
//    private bool playerInputEnabled; // Флаг для разрешения кликов игрока

//    private void Start()
//    {
        
//        sequence = new int[sequenceLength];
//        currentIndex = 0;
//        playerInputEnabled = false;

//        for (int i = 0; i < sequenceLength; i++)
//        {
//            sequence[i] = Random.Range(0, colorObjects.Length);
//        }

//        StartCoroutine(PlaySequence());
//    }

//    private void Update()
//    {
//        ClickSquare();
//    }

//    IEnumerator PlaySequence()
//    {
//        // Подсветка объектов по порядку из массива sequence
//        foreach (int index in sequence)
//        {
//            LightUpObject(index);
//            yield return new WaitForSeconds(lightUpDuration);
//            TurnOffObject(index);
//            yield return new WaitForSeconds(timeBetweenColors);
//        }

//        playerInputEnabled = true; // Разрешаем игроку кликать после окончания последовательности
//    }

//    void CheckPlayerInput(int index)
//    {
//        if (!playerInputEnabled)
//            return;

//        if (index == sequence[currentIndex])
//        {
//            currentIndex++;
//            Debug.Log("верно");
//            if (currentIndex == sequence.Length)
//            {
//                sequenceLength++;
//                currentIndex = 0;
//                playerInputEnabled = false;
//                StartCoroutine(PlaySequence());
//                Debug.Log("Вы Выиграли!");
//            }
//        }
//        else
//        {
            
//            // Здесь можно добавить дополнительные действия при проигрыше
//            // Например, перезагрузку уровня или окончание игры.
//        }
//    }

//}
