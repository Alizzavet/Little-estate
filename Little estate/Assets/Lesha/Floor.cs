using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Floor : MonoBehaviour
{
    [SerializeField] public Transform StartPoint;
    [SerializeField] public Transform EndPoint;

    [SerializeField] private List<Transform> _objectsSpawner;
    [SerializeField] private List<GameObject> _gameObjects;

    public void SpawnObjects()
    {
        if (_objectsSpawner == null)
            return;
        
        foreach (var spawnpos in _objectsSpawner)
        {
            var spawnChance = Random.Range(1, 5);

            if (spawnChance >= 2)
                continue;
            
            if (_gameObjects == null)
                return;
            
            var randomItem = Random.Range(0, _gameObjects.Count);

            var gameobject = Instantiate(_gameObjects[randomItem]);
            gameobject.transform.position = spawnpos.position;

        }
    }
}
