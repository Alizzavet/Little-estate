using UnityEngine;

public abstract class Outbuilding : MonoBehaviour, IInteractable
{
    private OutbuildingConfig _config;
    private SpriteRenderer _spriteRenderer;
    private Renderer _renderer;
    private BoxCollider _collider;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<BoxCollider>();
    }
    
    public void SetConfig(OutbuildingConfig newConfig)
    {
        _config = newConfig;
        var building = _config;
        _spriteRenderer.sprite = building.BuildingSprite;
        CheckCollider();
    }
    
    public Renderer GetBuildingRenderer()
    {
        return _renderer;
    }
    
    private void CheckCollider()
    {
        var sprite = _spriteRenderer.sprite;
        var size = _collider.size;
        size = sprite.bounds.size;
        var zSize = 2f; 
        size = new Vector3(size.x, size.y, zSize);
        _collider.size = size;
        _collider.center = sprite.bounds.center;
    }

    public virtual void Interact()
    {
        // интеракция
    }
}
