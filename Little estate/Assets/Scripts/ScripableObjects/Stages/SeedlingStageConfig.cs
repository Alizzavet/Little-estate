using UnityEngine;

[CreateAssetMenu(fileName = "SeedlingStageConfig", menuName = "Configs/Seedling Stage Config")]
public class SeedlingStageConfig : PlantStageConfig
{
    [SerializeField] private float _timeToNextStage;
  
    public float TimeToNextStage => _timeToNextStage;
}
