using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessChanger : MonoBehaviour
{
    public GameObject[] objectsToChange;

    private void Start()
    {
        // »змен€ем €ркость объектов в случайном пор€дке
        ChangeBrightnessRandomly();
    }

    public void ChangeBrightnessRandomly()
    {
        // —оздаем массив дл€ хранени€ случайных индексов объектов
        int[] randomIndices = new int[objectsToChange.Length];

        // «аполн€ем массив случайными индексами
        for (int i = 0; i < objectsToChange.Length; i++)
        {
            randomIndices[i] = i;
        }

        // ћен€ем €ркость объектов в случайном пор€дке
        for (int i = 0; i < objectsToChange.Length; i++)
        {
            // √енерируем случайный индекс из оставшихс€ элементов массива
            int randomIndex = Random.Range(i, objectsToChange.Length);

            // ћен€ем местами текущий элемент и элемент с случайным индексом
            GameObject tempObject = objectsToChange[i];
            objectsToChange[i] = objectsToChange[randomIndex];
            objectsToChange[randomIndex] = tempObject;

            // »змен€ем €ркость объекта
            ChangeBrightness(objectsToChange[i]);
        }
    }

    private void ChangeBrightness(GameObject obj)
    {
        // ѕолучаем компонент Renderer дл€ изменени€ €ркости
        Renderer renderer = obj.GetComponent<Renderer>();

        if (renderer != null)
        {
            // √енерируем случайное значение €ркости
            float randomBrightness = Random.Range(0f, 1f);

            // ”станавливаем новое значение €ркости объекта
            renderer.material.SetFloat("_Brightness", randomBrightness);
        }
    }
}
