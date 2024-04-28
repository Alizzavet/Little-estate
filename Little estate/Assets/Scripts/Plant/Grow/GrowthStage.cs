using UnityEngine;

public abstract class GrowthStage
{
    public abstract GrowthStage Grow(SpriteRenderer spriteRenderer, PlantConfig plantConfig);
}
