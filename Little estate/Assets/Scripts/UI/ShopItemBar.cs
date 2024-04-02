using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemBar : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _cost;

    public void SetData(PlantConfig plantConfig)
    {
        _itemIcon.sprite = plantConfig.SeedStageConfig.Sprite;
        _cost.text = plantConfig.SeedStageConfig.PriseCost.ToString();
        _itemName.text = plantConfig.ShopItemName;
        _description.text = plantConfig.ShopDescription;
    }
}
