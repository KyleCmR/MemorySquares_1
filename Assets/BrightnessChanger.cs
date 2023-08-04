using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessChanger : MonoBehaviour
{
    public GameObject[] objectsToChange;

    private void Start()
    {
        // �������� ������� �������� � ��������� �������
        ChangeBrightnessRandomly();
    }

    public void ChangeBrightnessRandomly()
    {
        // ������� ������ ��� �������� ��������� �������� ��������
        int[] randomIndices = new int[objectsToChange.Length];

        // ��������� ������ ���������� ���������
        for (int i = 0; i < objectsToChange.Length; i++)
        {
            randomIndices[i] = i;
        }

        // ������ ������� �������� � ��������� �������
        for (int i = 0; i < objectsToChange.Length; i++)
        {
            // ���������� ��������� ������ �� ���������� ��������� �������
            int randomIndex = Random.Range(i, objectsToChange.Length);

            // ������ ������� ������� ������� � ������� � ��������� ��������
            GameObject tempObject = objectsToChange[i];
            objectsToChange[i] = objectsToChange[randomIndex];
            objectsToChange[randomIndex] = tempObject;

            // �������� ������� �������
            ChangeBrightness(objectsToChange[i]);
        }
    }

    private void ChangeBrightness(GameObject obj)
    {
        // �������� ��������� Renderer ��� ��������� �������
        Renderer renderer = obj.GetComponent<Renderer>();

        if (renderer != null)
        {
            // ���������� ��������� �������� �������
            float randomBrightness = Random.Range(0f, 1f);

            // ������������� ����� �������� ������� �������
            renderer.material.SetFloat("_Brightness", randomBrightness);
        }
    }
}
