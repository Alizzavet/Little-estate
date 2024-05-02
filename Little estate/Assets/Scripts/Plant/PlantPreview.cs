using UnityEngine;

public class PlantPreview : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Camera _camera;
    private Vector3 _lastMousePosition;
    private Renderer _renderer;

    private void Awake()
    {
        _camera = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _renderer = GetComponent<Renderer>();
    }
    
    private void Update()
    { 
        if (Input.mousePosition != _lastMousePosition)
        {
            _lastMousePosition = Input.mousePosition;
    
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit3D))
            {
                if (hit3D.collider.gameObject.CompareTag("Ground"))
                {
                    var worldPos = hit3D.point;
                    worldPos.y = hit3D.collider.bounds.max.y + _renderer.bounds.size.y / 2; 
                    transform.position = worldPos;
                }
            }
        }
    }
    
    public void SetPlantSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}


