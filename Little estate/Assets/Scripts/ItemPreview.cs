using UnityEngine;

public abstract class ItemPreview : MonoBehaviour, IInputable
{ 
    private float _moveInterval = 0.3f; 
    private float _movementStep = 3f;
    private float _lastMoveTime;
    
    protected int _environmentCounter;
    protected bool _isAvailable = true;

    protected SpriteRenderer _spriteRenderer;
    protected Vector3 _yPosition;
    protected BoxCollider _collider;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider>();
    }
    
    public void OnEnable()
    {
        InputSystem.Instance.SetTimedInput(this);
    }

    public void HandleInput()
    {
        var horizontalInput = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        var verticalInput = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));

        if (horizontalInput == 0 && verticalInput == 0)
            return;
        
        var currentTime = Time.time;
        if (currentTime - _lastMoveTime >= _moveInterval)
        {
            MoveObject(horizontalInput, verticalInput);
            _lastMoveTime = currentTime;
        }
    }

    public bool IsAvailable()
    {
        return _isAvailable;
    }

    protected void UpdateColor()
    {
        if (_environmentCounter > 0)
            _spriteRenderer.color = Color.red;
        
        else
            _spriteRenderer.color = Color.green;
    }

    
    private void MoveObject(int horizontalInput, int verticalInput)
    {
        var movement = new Vector3(horizontalInput * _movementStep, 0f, verticalInput * _movementStep);
        transform.Translate(movement);

        // Устанавливаем позицию на фиксированной высоте над землей
        var currentPos = transform.position;
        currentPos.y = _yPosition.y;
        transform.position = currentPos;
    }
    
    private void OnDisable()
    {
        InputSystem.Instance.ReturnInput();
    }
}
