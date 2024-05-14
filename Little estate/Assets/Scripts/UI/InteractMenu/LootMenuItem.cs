using UnityEngine;
using Pool;

public class LootMenuItem : MonoBehaviour, IInteractMenuItem
{
    private PlantConfig _currentPlant;
    private Transform _transform;
    private Plant _plant;

    public string GetText()
    {
        return "Собрать урожай";
    }

    public void Execute()
    {
        for (var i = 0; i < _currentPlant.CountToSpawn; i++)
        {
            var item = PoolObject.Get<DropSeed>();
            item.SetData(_currentPlant, _transform);
        }
        
        PoolObject.Release(_plant);
    }

    public void GetConfig(PlantConfig plantConfig, Plant plant, Transform plantTransform)
    {
        _currentPlant = plantConfig;
        _transform = plantTransform;
        _plant = plant;
    }
}
