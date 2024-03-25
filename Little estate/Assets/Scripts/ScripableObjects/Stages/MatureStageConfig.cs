using UnityEngine;

[CreateAssetMenu(fileName = "MatureStageConfig", menuName = "Configs/Mature Stage Config")]
public class MatureStageConfig : SeedlingStageConfig
{
    [SerializeField] private string _lootName;
    [SerializeField] private int _minLootCount;
    [SerializeField] private int _maxLootCount;

    public string LootName => _lootName;

    public int MinLootCount
    {
        get { return _minLootCount; }
        set { _minLootCount = Mathf.Max(value, 1); } // Не даем ввести значение меньше 1
    }

    public int MaxLootCount
    {
        get { return _maxLootCount; }
        set { _maxLootCount = Mathf.Max(value, MinLootCount); } // Не даем ввести значение меньше чем MinLootCount
    }
}
