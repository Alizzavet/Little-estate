using UnityEngine;

public class MatureStage : GrowthStage
{
    public override GrowthStage Grow(SpriteRenderer spriteRenderer, PlantConfig plantConfig)
    {
        return this; 
    }
}
