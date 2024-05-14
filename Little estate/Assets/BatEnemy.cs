using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class BatEnemy : Enemy
{

    [SerializeField] private Collider _collider;

    private bool _isPunching;


    private Transform _player;
    private CharacterController _characterController;

    [SerializeField] private float _punchDist = 5f;
    [SerializeField] private float _punchSpeed = 5f;

    private States _currentState;
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _currentState = States.Patrol;
    }

    private int _punchRadius = 7;

    private int _patrolRadius = 20;

    [SerializeField] private float _moveSpeed;

    public void SetPlayer(Transform player)
    {
        _player = player;
    }
    public void UnsetPlayer()
    {
        _player = null;
    }

    private bool _isGrounded;
    [SerializeField] private Transform _groundetPos;
    [SerializeField] private LayerMask _layerMask;

    private Vector3 _verticalVelocity;

    private void Gravitation()
    {
        _isGrounded = Physics.CheckSphere(_groundetPos.position, 0.5f, _layerMask);

        if (_isGrounded)
            _verticalVelocity.y += -0.5f;
        else
            _verticalVelocity.y += -9.81f;
        
        
        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }
    private void Update()
    {

        Gravitation();
        
        switch (_currentState)
        {
            case States.Patrol:
                Patrol();
                break;
            case States.Chase:
                Chase();
                break;
            case States.Attack:
                if (_player != null && !_isPunching)
                {
                    _isPunching = true;
                    StartCoroutine(PunchPlayer());
                }
                break;
        }
    }

    private void Patrol()
    {
        var check = Physics.OverlapSphere(transform.position, _patrolRadius);

        foreach (var obj in check)
        {
            var player = obj.GetComponent<PlayerMoveController>();
            if (player != null)
            {
                SetPlayer(player.transform);
                _currentState = States.Chase;
            }
        }
    }

    private void Chase()
    {
        if (_player != null)
        {
            if (Physics.Raycast(transform.position, _player.position - transform.position, out var hit, _patrolRadius))
            {
                if (hit.transform == _player)
                {
                    _characterController.Move(((_player.position - transform.position).normalized * (Time.deltaTime * _moveSpeed)));
                    
                    var checkPunch = Physics.OverlapSphere(transform.position, _punchRadius);
                    foreach (var obj in checkPunch)
                    {
                        var player = obj.GetComponent<PlayerMoveController>();
                        if (player != null)
                            _currentState = States.Attack;
                    }
                }
            }
        }
    }



    IEnumerator PunchPlayer()
    {

        var playerpos = _player.position;
        var direction = (playerpos - transform.position).normalized;
        
        var remainingDistance = _punchDist;
        yield return new WaitForSeconds(1);

        while (remainingDistance > 0)
        {
            float distanceToMove = _punchSpeed * Time.deltaTime;
            _characterController.Move(direction * distanceToMove);
            remainingDistance -= distanceToMove;
            yield return null;
        }
        
        yield return new WaitForSeconds(1);
        _currentState = States.Patrol;
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

public enum States
{
    Patrol,
    Chase,
    Attack
}

