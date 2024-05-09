using UnityEngine;

public class SeedlingStage : GrowthStage
{
    public override GrowthStage Grow(SpriteRenderer spriteRenderer, PlantConfig plantConfig)
    {
        spriteRenderer.sprite = plantConfig.SeedlingStageConfig.Sprite;
        return new MatureStage();
    }
}