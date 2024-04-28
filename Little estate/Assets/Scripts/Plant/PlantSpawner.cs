using System;
using UnityEngine;
using Pool;

public class PlantSpawner : MonoBehaviour
{
    public static PlantSpawner Instance { get; private set; }
    
    private PlantPreview _plantPreview;
    private PlantConfig _plantConfig;

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
        return plant;
    }

    private void GrowPlant(Vector3 position)
    {
        var plant = SpawnPlant(_plantConfig);
        plant.transform.position = position;
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
