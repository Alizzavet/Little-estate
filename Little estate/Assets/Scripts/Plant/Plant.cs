using UnityEngine;

public abstract class Plant : MonoBehaviour
{
   [SerializeField] private Collider2D _collider2D;
   [SerializeField] private Rigidbody2D _rigidbody2D;
   
   public PlantConfig plantConfig;
   private SpriteRenderer _spriteRenderer;
   
   private void Awake()
   {
      _spriteRenderer = GetComponent<SpriteRenderer>();
   }

   public void SetConfig(PlantConfig newConfig)
   {
      plantConfig = newConfig;

      _spriteRenderer.sprite = plantConfig.SeedStageConfig.Sprite;
   }

   public abstract void CheckPlace();

   public abstract void Grow();

   public abstract void Loot();
}
