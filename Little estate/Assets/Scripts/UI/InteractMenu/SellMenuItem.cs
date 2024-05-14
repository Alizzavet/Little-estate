using Pool;
using UnityEngine;

public class SellMenuItem : MonoBehaviour, IInteractMenuItem
{
    private PlantConfig _currentPlant;
    private Plant _plant;
    
    public string GetText()
    {
        return "Продать";
    }

    public void Execute()
    {
        Coin.Instance.GetCoin(_currentPlant.ShopCost * 1.5f);
        PoolObject.Release(_plant);
    }

    public void GetConfig(PlantConfig plantConfig, Plant plant)
    {
        _currentPlant = plantConfig;
        _plant = plant;
    }
}
