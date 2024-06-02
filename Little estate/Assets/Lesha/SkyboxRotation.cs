using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    public GameObject objectPrefab; // Префаб объекта
    private Vector3 startPosition = new Vector3(-967, -27.8f, -31); // Начальная позиция
    private Vector3 targetPosition = new Vector3(695, -27.8f, -455); // Целевая позиция
    public float speed = 5f; // Скорость движения

    private GameObject currentObject; // Текущий движущийся объект

    void Start()
    {
        CreateNewObject();
    }

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * _speed);
        if (currentObject != null)
        {
            // Двигаем объект к целевой позиции
            currentObject.transform.position = Vector3.MoveTowards(currentObject.transform.position, targetPosition, speed * Time.deltaTime);

            // Проверяем, достиг ли объект целевой позиции
            if (Vector3.Distance(currentObject.transform.position, targetPosition) < 0.01f)
            {
                // Удаляем текущий объект
                Destroy(currentObject);

                // Создаем новый объект в стартовой позиции
                CreateNewObject();
            }
        }
    }

    void CreateNewObject()
    {
        currentObject = Instantiate(objectPrefab, startPosition, Quaternion.identity);
    }

}

