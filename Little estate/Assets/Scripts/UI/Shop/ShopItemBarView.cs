using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemBarView : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Image _itemCoin;
    [SerializeField] private Sprite _coin;
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _cost;

    public void SetData(PlantConfig plantConfig)
    {
        _itemIcon.sprite = plantConfig.MatureStageConfig.Sprite;
        _cost.text = plantConfig.ShopCost.ToString();
        _itemName.text = plantConfig.ShopItemName;
        _description.text = plantConfig.ShopDescription;
        _itemCoin.sprite = _coin;
    }
}
