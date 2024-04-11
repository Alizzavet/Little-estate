using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IInputable
{
    [SerializeField] private Rigidbody _rb;
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

    private void OnEnable()
    {
        InputSystem.Instance.SetInput(this);
    }

    private void FixedUpdate()
    {
        var colliders = Physics.OverlapSphere(_jumperPos.position, _radius, groundLayer);
        isGround = colliders.Length > 0;
        
        /*_rb.AddForce(_movementVector.normalized * _speed, ForceMode.Acceleration);*/

        var horizontalVelocity = Vector3.Lerp(_rb.velocity, _movementVector.normalized * _speed, 0.5f);
        _rb.velocity = new Vector3(horizontalVelocity.x, _rb.velocity.y, horizontalVelocity.z);
    }
    

    private void Jump()
    {
        _rb.AddForce(new Vector3(0, 10, 0), ForceMode.VelocityChange);
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
        
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            Jump();
    }
}
