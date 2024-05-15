using UnityEngine;

public class OutbuildingPreview : ItemPreview
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _unAvailable;

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

    public void SetBuildSprite(Sprite sprite)
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
            position.y = hit.collider.bounds.max.y;
            transform.position = position;
            _yPosition = position;
        }
    }
}
