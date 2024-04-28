using UnityEngine;
using UnityEngine.UI;

public class ShopItemBar : MonoBehaviour
{
    [SerializeField] private ShopItemBarView _itemBarView;
    [SerializeField] private Button _shopItem;
    
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
       PlantSpawner.Instance.CreatePlantPreview(_plantConfig); 
    }

    private void OnDisable()
    {
        _shopItem.onClick.RemoveListener(OnShopItemButtonClicked);
    }
}
