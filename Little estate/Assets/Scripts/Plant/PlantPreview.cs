using UnityEngine;

public class PlantPreview : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
 
    private void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = -_camera.transform.position.z;
        
        var worldPos = _camera.ScreenToWorldPoint(mousePos);
        worldPos.x = Mathf.Round(worldPos.x * 2) / 2;
        worldPos.y = Mathf.Round(worldPos.y * 2) / 2;
        transform.position = worldPos;
    }


    public void SetPlantSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
