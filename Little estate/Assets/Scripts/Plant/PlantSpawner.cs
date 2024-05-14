using System;
using UnityEngine;
using Pool;

public class PlantSpawner : MonoBehaviour
{
    [SerializeField] private Transform _playerCollision;

    public static PlantSpawner Instance { get; private set; }
    public event Action<PlantConfig> IsGrow;

    private PlantPreview _plantPreview;
    private PlantConfig _plantConfig;
    private Renderer _renderer;
    private bool _limiter;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        PlayerInventory.Instance.IsInventoryUse += InventoryLimiter;
    }

    private void InventoryLimiter(bool value)
    {
        _limiter = value;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _plantPreview != null && _plantPreview.IsAvailable())
        {
            GrowPlant(_plantPreview.transform.position);
            DestroyPlantPreview();
            
            if(_limiter) 
                IsGrow.Invoke(_plantConfig);
        }
     
        if (Input.GetKeyDown(KeyCode.Escape) && _plantPreview != null)
        {
            if (!_limiter)
            {
                DestroyPlantPreview(); 
                Coin.Instance.GetCoin(_plantConfig.ShopCost);
            }
            else
            {
                DestroyPlantPreview();
                InventoryLimiter(false);
            }
        }
    }

    public Plant SpawnPlant(PlantConfig config)
    {
        var plant = PoolObject.Get<Plant>();
        plant.SetConfig(config);
        _renderer = plant.GetPlantRenderer();
        
        return plant;
    }

    private void GrowPlant(Vector3 position)
    {
        var plant = SpawnPlant(_plantConfig);
        
        if (Physics.Raycast(position, Vector3.down, out var hit))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                var worldPos = hit.point;
                worldPos.y = hit.collider.bounds.max.y + _renderer.bounds.size.y / 2; 
                transform.position = worldPos;
                plant.transform.position = worldPos;
            }
        }
    }

    public void CreatePlantPreview(PlantConfig plantConfig)
    {
        var plantPreview = PoolObject.Get<PlantPreview>();
        plantPreview.transform.position = _playerCollision.position;
        plantPreview.SetPlantSprite(plantConfig.MatureStageConfig.Sprite);
        _plantPreview = plantPreview;
        _plantConfig = plantConfig;
    }

    private void DestroyPlantPreview()
    {
        if(_plantPreview != null)
            PoolObject.Release(_plantPreview);
        
        _plantPreview = null;
    }

    private void OnDestroy()
    {
        PlayerInventory.Instance.IsInventoryUse -= InventoryLimiter;
    }
}
