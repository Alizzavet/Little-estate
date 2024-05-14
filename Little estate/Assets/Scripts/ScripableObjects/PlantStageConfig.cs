using UnityEngine;

public abstract class PlantStageConfig : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _priceCost;
    
    public Sprite Sprite => _sprite;
    public int PriseCost => _priceCost;
}

