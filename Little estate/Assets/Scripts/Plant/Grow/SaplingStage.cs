using UnityEngine;

public class SaplingStage : GrowthStage
{
    public override GrowthStage Grow(SpriteRenderer spriteRenderer, PlantConfig plantConfig)
    {
        spriteRenderer.sprite = plantConfig.MatureStageConfig.Sprite;
        return new MatureStage();
    }
}
