using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private List<Floor> _floors;
// 50 0 -2

    private List<Floor> _spawnedFloors = new List<Floor>();
    [SerializeField] private int _roomsCount;


    private void Start()
    {
        Generate();
        // отнимаю 35, чтобы не показывать край карты
        CameraFollowController.Instance.SetX(_spawnedFloors[_spawnedFloors.Count - 1].EndPoint.position.x - 35f);
    }

    private void Generate()
    {
        for (var i = 0; i <= _roomsCount; i++)
        {
            SpawnRoom();
        }
    }

    private void SpawnRoom()
    {
        var room = Instantiate(_floors[Random.Range(0, _floors.Count)]);
        if (_spawnedFloors.Count == 0)
            room.transform.position = new Vector3(50, 0, -2);
        else
        {
            room.transform.position = _spawnedFloors[_spawnedFloors.Count - 1].EndPoint.position -
                                      room.StartPoint.localPosition;
        }
        
        _spawnedFloors.Add(room);
    }
}
