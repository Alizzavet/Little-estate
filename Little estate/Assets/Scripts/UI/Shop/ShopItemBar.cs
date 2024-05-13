using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemBar : MonoBehaviour
{
    [SerializeField] private ShopItemBarView _itemBarView;
    [SerializeField] private Button _shopItem;

    public event Action IsShop;
    
    private PlantConfig _plantConfig;

    private void OnEnable()
    {
        _shopItem.onClick.AddListener(OnShopItemButtonClicked);
    }

    public void SetData(PlantConfig plantConfig)
    {
        _plantConfig = plantConfig;
        _itemBarView.SetData(plantConfig);
    }

    private void OnShopItemButtonClicked()
    {
        if (Coin.Instance.CurrentCoinCount() < _plantConfig.ShopCost)
            return;

        PlantSpawner.Instance.CreatePlantPreview(_plantConfig);
        Coin.Instance.SpendCoin(_plantConfig.ShopCost);
        
        if (IsShop != null)
            IsShop.Invoke();
    }

    private void OnDisable()
    {
        _shopItem.onClick.RemoveListener(OnShopItemButtonClicked);
    }
}
