using UnityEngine;

public abstract class PlantStageConfig : ScriptableObject
{
    [SerializeField] private PlantGrowthStage  _stageName;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _priceCost;

    public PlantGrowthStage  StageName => _stageName;
    public Sprite Sprite => _sprite;
    public int PriseCost => _priceCost;
}

public enum PlantGrowthStage
{
    Seed,
    Seedling,
    Sapling,
    Mature
}
