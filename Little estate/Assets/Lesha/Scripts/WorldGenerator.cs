using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
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
    [SerializeField] private EndRoom _endRoom;

    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private CinemachineVirtualCamera _camera;

    private void Start()
    {
        var startRoom = Instantiate(_startRoom.gameObject, new Vector3(120f, 0f, 0f), Quaternion.Euler(0, 90, 0));
        _startRoom = startRoom.GetComponent<StartRoom>();
        Generate();
        var endRoom = Instantiate(_endRoom.gameObject, new Vector3(0f, 0f, 0f), Quaternion.Euler(0, -90, 0));
        endRoom.transform.position = _spawnedFloors[_spawnedFloors.Count - 1].EndPoint.position -
            endRoom.GetComponent<EndRoom>()._endSpawnPos.localPosition + new Vector3(39, 0.6f, 38);

        var player = Instantiate(_playerPrefab, _startRoom._playerSpawnPos.position, quaternion.identity);
        _camera.Follow = player.transform;
    }

    private void Generate()
    {
        for (var i = 0; i <= _roomsCount; i++)
            SpawnRoom();
    }

    private void SpawnRoom()
    {
        var room = Instantiate(_floors[Random.Range(0, _floors.Count)]);

        if (_spawnedFloors.Count == 0)
        {
            //room.transform.position =  //new Vector3(50, 0, -2);
            room.transform.position = _startRoom._roomSpawnPos.position -
                room.StartPoint.localPosition;
        }
        else
            room.transform.position = _spawnedFloors[_spawnedFloors.Count - 1].EndPoint.position -
                                      room.StartPoint.localPosition;
        
        room.GetComponent<Floor>().SpawnObjects();
        _spawnedFloors.Add(room);
    }
}
