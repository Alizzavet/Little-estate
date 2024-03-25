using UnityEngine;

[CreateAssetMenu(fileName = "PlantConfig", menuName = "Configs/Plant Config")]
public class PlantConfig : ScriptableObject
{
    [SerializeField] private SeedStageConfig _seedStageConfig;
    [SerializeField] private SeedlingStageConfig _seedlingStageConfig;
    [SerializeField] private SaplingStageConfig _saplingStageConfig;
    [SerializeField] private MatureStageConfig _matureStageConfig;

    public SeedStageConfig SeedStageConfig => _seedStageConfig;
    public SeedlingStageConfig SeedlingStageConfig => _seedlingStageConfig;
    public SaplingStageConfig SaplingStageConfig => _saplingStageConfig;
    public MatureStageConfig MatureStageConfig => _matureStageConfig;
}
