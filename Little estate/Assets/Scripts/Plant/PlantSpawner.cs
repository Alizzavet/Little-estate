using UnityEngine;
using Pool;

public class PlantSpawner : MonoBehaviour
{
    public static PlantSpawner Instance { get; private set; }
    
    private PlantPreview _plantPreview;
    private PlantConfig _plantConfig;
    private Renderer _renderer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _plantPreview != null)
        {
            GrowPlant(_plantPreview.transform.position);
            DestroyPlantPreview();
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
}
