using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IInputable
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Transform _jumperPos;
    [SerializeField] private List<GameObject> _nonFlippingObjects;
    
    private bool _mFacingRight = true;
    public bool isGround;
    [SerializeField] private LayerMask groundLayer;

    private Vector3 _movementVector;
    
    public float _radius = 0.6f;



    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        InputSystem.Instance.SetInput(this);
        CameraFollowController.Instance.SetPlayer(transform);
        CameraFollowController.Instance.MoveToPlayer();
        
        Debug.Log("Игрок");
    }

    /*private void FixedUpdate()
    {
        var colliders = Physics.OverlapSphere(_jumperPos.position, _radius, groundLayer);
        isGround = colliders.Length > 0;
        
        /*_rb.AddForce(_movementVector.normalized * _speed, ForceMode.Acceleration);#1#

        var horizontalVelocity = Vector3.Lerp(_rb.velocity, _movementVector.normalized * _speed, 0.5f);
        _rb.velocity = new Vector3(horizontalVelocity.x, _rb.velocity.y, horizontalVelocity.z);
    }*/
    

    /*private void Jump()
    {
        /*_rb.AddForce(new Vector3(0, 10, 0), ForceMode.VelocityChange);#1#
    }*/
    
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

    private void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        _movementVector = new Vector3(horizontal, 0, vertical);
        switch (horizontal)
        {
            case > 0 when !_mFacingRight:
            case < 0 when _mFacingRight:
                Flip();
                break;
        }

        var isMoving = horizontal != 0 || vertical != 0;
        _animator.SetBool("moving", isMoving);
        
        _animator.SetFloat("horizontal", horizontal);
        _animator.SetFloat("vertical", vertical);

        _characterController.Move(_movementVector.normalized * _speed * Time.deltaTime);
    }

    private Vector3 _verticalVelocity;
    private bool _isGrounded;
    private void Gravitation()
    {
        _isGrounded = Physics.CheckSphere(_jumperPos.position, 0.3f, LayerMask.GetMask("Default"));
        
        if (_jumperPos && Input.GetKeyDown(KeyCode.Space) && _isGrounded) // Проверяем, что персонаж на земле и нажата кнопка прыжка
        {
            _verticalVelocity.y = Mathf.Sqrt(5 * -2f * -9.81f); // Вычисляем вертикальную скорость для прыжка
        }
        else
        {
            _verticalVelocity.y += -9.81f * Time.deltaTime; // Применяем гравитацию
        }
        
        _characterController.Move(_verticalVelocity);
    }
    public void HandleInput()
    {

        Move();
        Gravitation();

    }
}
