using UnityEngine;

[CreateAssetMenu(fileName = "PlantConfig", menuName = "Configs/Plant Config")]
public class PlantConfig : DropedItemConfig
{
    [SerializeField] private SeedlingStageConfig _seedlingStageConfig;
    [SerializeField] private MatureStageConfig _matureStageConfig;
    [SerializeField] private string _shopItemName;
    [SerializeField] private string _shopDescription;
    [SerializeField] private int _shopCost;

    public SeedlingStageConfig SeedlingStageConfig => _seedlingStageConfig;
    public MatureStageConfig MatureStageConfig => _matureStageConfig;
    public string ShopItemName => _shopItemName;
    public string ShopDescription => _shopDescription;
    public int ShopCost => _shopCost;
}
