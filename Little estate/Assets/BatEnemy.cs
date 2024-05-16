using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BatEnemy : Enemy
{

    [SerializeField] private float _punchDist = 5f;
    [SerializeField] private float _punchSpeed = 5f;
    [SerializeField] private Transform _groundetPos;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _moveSpeed;


    private bool _isPunching;
    private bool _mFacingRight;

    private Transform _player;
    private CharacterController _characterController;

    private LineRenderer _line;
    private States _currentState;
    
    private bool _isGrounded;

    private int _punchRadius = 7;

    private int _patrolRadius = 20;

    private int damage = 10;

    private Vector3 _verticalVelocity;
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _line = GetComponent<LineRenderer>();
        _currentState = States.Patrol;
    }



    public void SetPlayer(Transform player)
    {
        _player = player;
    }
    public void UnsetPlayer()
    {
        _player = null;
    }


    public override void OnDeath()
    {
        SpawnItems();
    }
    private void Gravitation()
    {
        _isGrounded = Physics.CheckSphere(_groundetPos.position, 0.5f, _layerMask);

        if (_isGrounded)
            _verticalVelocity.y += -0.5f;
        else
            _verticalVelocity.y += -9.81f;
        
        
        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }
    private bool isPatrolling = false;
    private void Update()
    {

        Gravitation();
        
        switch (_currentState)
        {
            case States.Patrol:
                if (!isPatrolling)
                    StartCoroutine(Patrol());
                
                FindPlayer();
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
    
    private void Flip()
    {
        _mFacingRight = !_mFacingRight;

        var transform1 = transform;
        var theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
        
    }

    #region Patrol
    private IEnumerator Patrol()
    {
        _characterController.Move(Vector3.zero);
        isPatrolling = true;
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

        switch (randomDirection.x)
        {
            // Проверяем, в какую сторону должен двигаться враг
            case > 0 when !_mFacingRight:
            case < 0 when _mFacingRight:
                Flip();
                break;
        }
        
        Ray ray = new Ray(transform.position, randomDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 4))
            _characterController.Move(hit.point * _moveSpeed * Time.deltaTime);
        else
            // Если луч не врезался - идем в ту сторону на расстоянии 4
            _characterController.Move(randomDirection * _moveSpeed * Time.deltaTime);
        
        yield return new WaitForSeconds(2);
    
        isPatrolling = false;
    }

    private void FindPlayer()
    {
        var check = Physics.OverlapSphere(transform.position, _patrolRadius);

        foreach (var obj in check)
        {
            var player = obj.GetComponent<PlayerMoveController>();
            if (player != null)
            {
                SetPlayer(player.transform);
                StopCoroutine(Patrol());
                _currentState = States.Chase;
            }
        }
    }
    

    #endregion

    #region Chase
    private void Chase()
    {
        
        if (_player != null)
        {
            if (Physics.Raycast(transform.position, _player.position - transform.position, out var hit, _patrolRadius))
            {
                if (hit.transform == _player)
                {
                    var direction = (_player.position - transform.position).normalized;

                    switch (direction.x)
                    {
                        // Проверяем, в какую сторону должен двигаться враг
                        case > 0 when !_mFacingRight:
                        case < 0 when _mFacingRight:
                            Flip();
                            break;
                    }

                    _characterController.Move((direction * (Time.deltaTime * _moveSpeed)));
                    
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

    

    #endregion

    private float _remainingDistance;
    #region Attack
    IEnumerator PunchPlayer()
    {

        var playerpos = _player.position;
        var direction = (playerpos - transform.position).normalized;
        
        switch (direction.x)
        {
            case > 0 when !_mFacingRight:
            case < 0 when _mFacingRight:
                Flip();
                break;
        }
        
        _remainingDistance = _punchDist;
        
        _line.SetPosition(0, transform.position); // начальная точка линии
        _line.SetPosition(1, playerpos); // конечная точка линии
        _line.enabled = true;
        
        
        yield return new WaitForSeconds(1);
        _line.enabled = false;

        while (_remainingDistance > 0)
        {
            float distanceToMove = _punchSpeed * Time.deltaTime;
            _characterController.Move(direction * distanceToMove);
            _remainingDistance -= distanceToMove;
            yield return null;
        }
        
        yield return new WaitForSeconds(1);
        _currentState = States.Patrol;
        _isPunching = false;

    }
    

    #endregion


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var player = hit.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage, hit.moveDirection);
            _remainingDistance = -1;
            StopCoroutine(PunchPlayer());
            _characterController.Move(Vector3.zero);
            _currentState = States.Patrol;
            
        }

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

