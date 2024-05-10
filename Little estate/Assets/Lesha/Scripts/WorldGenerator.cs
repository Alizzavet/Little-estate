using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private List<Floor> _floors;

    private List<Floor> _spawnedFloors = new List<Floor>();
    [SerializeField] private int _roomsCount;

    [SerializeField] private NavMeshSurface _navMeshSurface;

    private void Start()
    {
        Generate();
        // отнимаю 40, чтобы не показывать край карты
        CameraFollowController.Instance.SetX(_spawnedFloors[_spawnedFloors.Count - 1].EndPoint.position.x - 40f);
    }

    private void Generate()
    {
        for (var i = 0; i <= _roomsCount; i++)
        {
            SpawnRoom();
        }
        _navMeshSurface.BuildNavMesh();
    }

    private void SpawnRoom()
    {
        var room = Instantiate(_floors[Random.Range(0, _floors.Count)]);
        
        if (_spawnedFloors.Count == 0)
            room.transform.position = new Vector3(50, 0, -2);
        else
            room.transform.position = _spawnedFloors[_spawnedFloors.Count - 1].EndPoint.position -
                                      room.StartPoint.localPosition;
        
        room.GetComponent<Floor>().SpawnObjects();
        _spawnedFloors.Add(room);
    }
}
