using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private List<Floor> _floors;

    private List<Floor> _spawnedFloors = new();
    [SerializeField] private int _roomsCount;
    
    [SerializeField] private StartRoom _startRoom;

    [SerializeField] private GameObject _playerPrefab;

    private void Start()
    {
        var startRoom = Instantiate(_startRoom.gameObject, new Vector3(120f, 0f, 0f), Quaternion.Euler(0, 90, 0));

        
        Generate();
        // отнимаю 40, чтобы не показывать край карты
        //CameraFollowController.Instance.SetX(_spawnedFloors[_spawnedFloors.Count - 1].EndPoint.position.x - 40f);
        CameraFollowController.Instance.SetX(_spawnedFloors[_spawnedFloors.Count - 1].EndPoint.position.x - 40f);
        CameraFollowController.Instance.SetMinX(_startRoom._minXPos.position.x);
        _startRoom = startRoom.GetComponent<StartRoom>();

        
        // спавн игрока
        Instantiate(_playerPrefab, _startRoom._playerSpawnPos.position, quaternion.identity);
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
        {
            //room.transform.position =  //new Vector3(50, 0, -2);
            room.transform.position = _startRoom._roomSpawnPos.position -
                room.GetComponent<Floor>().StartPoint.localPosition;
        }
        else
            room.transform.position = _spawnedFloors[_spawnedFloors.Count - 1].EndPoint.position -
                                      room.StartPoint.localPosition;
        
        room.GetComponent<Floor>().SpawnObjects();
        _spawnedFloors.Add(room);
    }
}
