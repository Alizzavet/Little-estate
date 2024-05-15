using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemBar : MonoBehaviour
{
    [SerializeField] private ShopItemBarView _itemBarView;
    [SerializeField] private Button _shopItem;

    public event Action IsShop;
    
    private PlantConfig _plantConfig;
    private OutbuildingConfig _outbuildingConfig;
    private bool _isPlant;

    private void OnEnable()
    {
        _shopItem.onClick.AddListener(OnShopItemButtonClicked);
    }

    public void SetData(PlantConfig plantConfig)
    {
        _isPlant = true;
        _plantConfig = plantConfig;
        _itemBarView.SetData(plantConfig);
    }

    public void SedBuildingData(OutbuildingConfig outbuildingConfig)
    {
        _isPlant = false;
        _outbuildingConfig = outbuildingConfig;
        _itemBarView.SedBuildingData(outbuildingConfig);
    }
    

    private void OnShopItemButtonClicked()
    {
        if (_isPlant)
        {
            if (Coin.Instance.CurrentCoinCount() < _plantConfig.ShopCost)
                return;

            PlantSpawner.Instance.CreatePlantPreview(_plantConfig);
            Coin.Instance.SpendCoin(_plantConfig.ShopCost);
        
            if (IsShop != null)
                IsShop.Invoke();
        }
        else if(!_isPlant)
        {
            if (Coin.Instance.CurrentCoinCount() < _outbuildingConfig.ShopCost)
                return;
            
            OutbuildingSpawner.Instance.CreateBuildingPreview(_outbuildingConfig);
            Coin.Instance.SpendCoin(_outbuildingConfig.ShopCost);
            
            if (IsShop != null)
                IsShop.Invoke();
        }
    }

    private void OnDisable()
    {
        _shopItem.onClick.RemoveListener(OnShopItemButtonClicked);
    }
}
