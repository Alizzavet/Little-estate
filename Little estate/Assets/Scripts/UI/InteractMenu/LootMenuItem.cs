using UnityEngine;
using Pool;

public class LootMenuItem : MonoBehaviour, IInteractMenuItem
{
    private PlantConfig _currentPlant;
    private Transform _transform;

    public string GetText()
    {
        return "Собрать урожай";
    }

    public void Execute()
    {
        var item = PoolObject.Get<DropSeed>();
        item.SetData(_currentPlant, _transform);
    }

    public void GetConfig(PlantConfig plantConfig, Plant plant, Transform plantTransform)
    {
        _currentPlant = plantConfig;
        _transform = plantTransform;
    }
}
