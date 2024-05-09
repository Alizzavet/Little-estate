using UnityEngine;

public class PlantPreview : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float _moveInterval = 0.3f;
    [SerializeField] private float _movementStep = 50f;
    
    private float _lastMoveTime;
    private float _fixedHeight;                         

    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastMousePosition;
    private Vector3 _yPosition;
    private BoxCollider _collider;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider>();
    }

    private void Update()
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
    
    private void MoveObject(int horizontalInput, int verticalInput)
    {
        Vector3 movement = new Vector3(horizontalInput * _movementStep, 0f, verticalInput * _movementStep);
        transform.Translate(movement);

        // Устанавливаем позицию на фиксированной высоте над землей
        Vector3 currentPos = transform.position;
        currentPos.y = _yPosition.y;
        transform.position = currentPos;
    }

    
    public void SetPlantSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        
        _collider.size = sprite.bounds.size;
        _collider.center = sprite.bounds.center;
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 position = transform.position;
            position.y = hit.collider.bounds.max.y + (_collider.bounds.size.y / 2);
            transform.position = position;
            _yPosition = position;
        }
    }
}


