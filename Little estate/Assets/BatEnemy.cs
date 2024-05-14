using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class BatEnemy : Enemy
{

    [SerializeField] private Collider _collider;



    private int _punchRadius = 10;

    private int _patrolRadius = 20;
    private Transform _player;
    [SerializeField] private float _moveSpeed;

    public void SetPlayer(Transform player)
    {
        _player = player;
    }
    public void UnsetPlayer()
    {
        _player = null;
    }
    [SerializeField] private LayerMask _layerMask;

    private void Update()
    {
        if (_isPunching) return;
        
        
        var check = Physics.OverlapSphere(transform.position, _patrolRadius, _layerMask);

        foreach (var obj in check)
        {
            var player = obj.GetComponent<PlayerMoveController>();
            if (player != null)
                SetPlayer(player.transform);
            
        }

        if (_player != null)
        {
            if (Physics.Raycast(transform.position, _player.position - transform.position, out var hit, _patrolRadius))
            {
                if (hit.transform == _player)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _player.position, Time.deltaTime * _moveSpeed);
                    
                    var checkPunch = Physics.OverlapSphere(transform.position, _punchRadius, _layerMask);
                    foreach (var obj in checkPunch)
                    {
                        var player = obj.GetComponent<PlayerMoveController>();
                        if (player != null)
                            StartCoroutine(PunchPlayer());
                    }
                }
            }
        }
    }
    private bool _isPunching = false;

    IEnumerator PunchPlayer()
    {
        _isPunching = true;
        
        Vector3 originalPosition = transform.position;
        Vector3 punchPosition = _player.position + (_player.position - transform.position).normalized * 5;
        float moveSpeed = 5f;
        float startTime = Time.time;

        while (Time.time < startTime + 1)
        {
            // Вычисляем направление движения
            var direction = (punchPosition - originalPosition).normalized;
        
            // Обновляем позицию объекта
            transform.position += direction * (moveSpeed * 3 * Time.deltaTime) ;
        
            yield return null;
        }
        yield return new WaitForSeconds(1);

        _isPunching = false;
    }


    public override void OnDeath()
    {
        Debug.Log("умер чел");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _patrolRadius);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _punchRadius);
    }
}


/*if (_player == null)
{
    _meshAgent.destination = transform.position;
}
else if (_meshAgent.destination != _player.transform.position + Vector3.one)
{
    _meshAgent.destination = _player.transform.position;
}*/
/*[SerializeField] private NavMeshAgent _meshAgent;*/