using UnityEngine;

[CreateAssetMenu(fileName = "OutbuildingConfig", menuName = "Configs/Outbuilding Config")]
public class OutbuildingConfig : ScriptableObject
{
    [SerializeField] private Sprite _buildingSprite;
    [SerializeField] private string _shopItemName;
    [SerializeField] private string _shopDescription;
    [SerializeField] private int _shopCost;

    public Sprite BuildingSprite => _buildingSprite;
    public string ShopItemName => _shopItemName;
    public string ShopDescription => _shopDescription;
    public int ShopCost => _shopCost;
}
