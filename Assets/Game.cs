//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class Game : MonoBehaviour
//{
//    public GameObject[] colorObjects; // ������ �������� � �������
//    public int sequenceLength = 4; // ����� ������������������ ������
//    public float lightUpDuration = 1.0f; // ����������������� ��������� � ��������
//    public float timeBetweenColors = 0.5f; // ����� ����� ����������� � ��������

//    private int[] sequence; // ������ ��� �������� ������������������ ���������
//    private int currentIndex; // ������ �������� ������� ��� ���������
//    private bool playerInputEnabled; // ���� ��� ���������� ������ ������

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
//        // ��������� �������� �� ������� �� ������� sequence
//        foreach (int index in sequence)
//        {
//            LightUpObject(index);
//            yield return new WaitForSeconds(lightUpDuration);
//            TurnOffObject(index);
//            yield return new WaitForSeconds(timeBetweenColors);
//        }

//        playerInputEnabled = true; // ��������� ������ ������� ����� ��������� ������������������
//    }

//    void CheckPlayerInput(int index)
//    {
//        if (!playerInputEnabled)
//            return;

//        if (index == sequence[currentIndex])
//        {
//            currentIndex++;
//            Debug.Log("�����");
//            if (currentIndex == sequence.Length)
//            {
//                sequenceLength++;
//                currentIndex = 0;
//                playerInputEnabled = false;
//                StartCoroutine(PlaySequence());
//                Debug.Log("�� ��������!");
//            }
//        }
//        else
//        {
            
//            // ����� ����� �������� �������������� �������� ��� ���������
//            // ��������, ������������ ������ ��� ��������� ����.
//        }
//    }

//}
