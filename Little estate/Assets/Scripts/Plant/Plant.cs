using UnityEngine;

public abstract class Plant : MonoBehaviour
{
   [SerializeField] private Collider2D _collider2D;
   [SerializeField] private Rigidbody2D _rigidbody2D;

   private PlantConfig _plantConfig;
   private SpriteRenderer _spriteRenderer;
   private GrowthStage _currentGrowthStage;

   private void Awake()
   {
      _spriteRenderer = GetComponent<SpriteRenderer>();
   }

   private void OnEnable()
   {
      DayNightCycle.NextDay += Grow;
   }

   public void SetConfig(PlantConfig newConfig)
   {
      _plantConfig = newConfig;
      var seedling = _plantConfig.SeedlingStageConfig;
      _spriteRenderer.sprite = seedling.Sprite;
      _currentGrowthStage = new SeedlingStage();
   }

   public virtual void Grow()
   {
      _currentGrowthStage = _currentGrowthStage.Grow(_spriteRenderer, _plantConfig);
   }

   public abstract void CheckPlace();
   
   public abstract void Loot();

   private void OnDisable()
   {
      DayNightCycle.NextDay -= Grow;
   }
}
