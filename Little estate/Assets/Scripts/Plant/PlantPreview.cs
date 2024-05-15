using UnityEngine;

public class PlantPreview : MonoBehaviour, IInputable
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _unAvailable; 
    [SerializeField] private float _moveInterval = 0.3f;
    [SerializeField] private float _movementStep = 3f;

    private float _lastMoveTime;
    private float _fixedHeight;   
    private int _environmentCounter;
    private bool _isAvailable = true;

    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastMousePosition;
    private Vector3 _yPosition;
    private BoxCollider _collider;

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

    private void OnTriggerEnter(Collider other)
    {
        if ((_unAvailable.value & (1 << other.gameObject.layer)) != 0)
        {
            _environmentCounter++;
            UpdateColor();
            _isAvailable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((_unAvailable.value & (1 << other.gameObject.layer)) != 0)
        {
            _environmentCounter--;
            UpdateColor();
            _isAvailable = true;
        }
    }

    public bool IsAvailable()
    {
        return _isAvailable;
    }

    private void UpdateColor()
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
    
    public void SetPlantSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        _spriteRenderer.color = Color.green;

        var size = _collider.size;
        size = sprite.bounds.size;
        var zSize = 2f; 
        size = new Vector3(size.x, size.y, zSize);
        _collider.size = size;
        _collider.center = sprite.bounds.center;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, _groundLayer))
        {
            Vector3 position = transform.position;
            position.y = hit.collider.bounds.max.y + (_collider.bounds.size.y / 2);
            transform.position = position;
            _yPosition = position;
        }
    }

    private void OnDisable()
    {
        InputSystem.Instance.ReturnInput();
    }
}


