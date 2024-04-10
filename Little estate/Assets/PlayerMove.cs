using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Transform _jumperPos;
    
    private bool _mFacingRight = true;
    public bool isGround;
    [SerializeField] private LayerMask groundLayer;
    
    private Vector3 _movementVector
    {
        get
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

            var isMoving = horizontal != 0 || vertical != 0;
            _animator.SetBool("moving", isMoving);
            
            _animator.SetFloat("horizontal", horizontal);
            _animator.SetFloat("vertical", vertical);
            return new Vector3(horizontal, 0.0f, vertical);
        }
    }
    public float _radius = 0.6f;
    private void FixedUpdate()
    {
        var colliders = Physics.OverlapSphere(_jumperPos.position, _radius, groundLayer);
        isGround = colliders.Length > 0;
        
        _rb.AddForce(_movementVector.normalized * _speed, ForceMode.Acceleration);
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            Jump();
    }

    private void Jump()
    {
        _rb.AddForce(new Vector3(0, 30, 0), ForceMode.VelocityChange);
    }
    
    private void Flip()
    {
        _mFacingRight = !_mFacingRight;
        
        var transform1 = transform;
        var theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
    }
}