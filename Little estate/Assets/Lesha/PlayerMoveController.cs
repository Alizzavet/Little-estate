using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour, IInputable
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _groundetPos;
    [SerializeField] private List<GameObject> _nonFlippingObjects;
    [SerializeField] private Animator _animator;
    private bool _isGrounded;
    private Vector3 _movementVector;
    public bool _mFacingRight = true;
    private Vector3 _verticalVelocity;

    [SerializeField] private LayerMask _layerMask;


    [SerializeField] private PlayerInventory _playerInventory;
    private void OnEnable()
    {
        InputSystem.Instance.SetInput(this);
        CameraFollowController.Instance.SetPlayer(transform);
        CameraFollowController.Instance.MoveToPlayer();
    }

    private void Update()
    {
        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }

    private void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        switch (horizontal)
        {
            case > 0 when !_mFacingRight:
            case < 0 when _mFacingRight:
                Flip();
                break;
        }

        _movementVector = new Vector3(horizontal, 0, vertical).normalized;

        _characterController.Move(_movementVector * (_speed * Time.deltaTime));


        var isMoving = horizontal != 0 || vertical != 0;
        _animator.SetBool("moving", isMoving);

        _animator.SetFloat("horizontal", horizontal);
        _animator.SetFloat("vertical", vertical);
    }

    private void Gravitation()
    {
        _isGrounded = Physics.CheckSphere(_groundetPos.position, 0.5f, _layerMask);

        if (_isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _verticalVelocity.y = Mathf.Sqrt(5 * -2f * -9.81f);
        }
        else
            _verticalVelocity.y += -15 * Time.deltaTime;

        
    }

    private void Flip()
    {
        _mFacingRight = !_mFacingRight;

        var transform1 = transform;
        var theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;

        foreach (var obj in _nonFlippingObjects)
        {
            var objScale = obj.transform.localScale;
            objScale.x *= -1;
            obj.transform.localScale = objScale;
        }
    }

    public void HandleInput()
    {
        Move();
        Gravitation();
    }

    private void OnTriggerEnter(Collider other)
    {
        var item = other.gameObject.GetComponent<DropedItem>();


        if (item == null || !item.CanTaking) 
            return;
        
        // занесение предмета в инвентарь и удаление из мира
        if(_playerInventory.AddItemFromWorld(item.DropedItemConfig))
            item.Take();



    }
}