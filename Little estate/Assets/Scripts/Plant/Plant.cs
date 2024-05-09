using UnityEngine;

public abstract class Plant : MonoBehaviour
{
   [SerializeField] private Collider2D _collider2D;
   [SerializeField] private Rigidbody2D _rigidbody2D;

   private PlantConfig _plantConfig;
   private SpriteRenderer _spriteRenderer;
   private GrowthStage _currentGrowthStage;
   private Renderer _renderer;

   private void Awake()
   {
      _spriteRenderer = GetComponent<SpriteRenderer>();
      _renderer = GetComponent<Renderer>();
   }

   private void OnEnable()
   {
      DayNightCycle.NextDay += Grow;
   }

   public void SetConfig(PlantConfig newConfig)
   {
      _plantConfig = newConfig;
      var seedling = _plantConfig;
      _spriteRenderer.sprite = seedling.SeedSprite;
      _currentGrowthStage = new SeedlingStage();
   }

   public Renderer GetPlantRenderer()
   {
      return _renderer;
   }

   public virtual void Grow()
   {
      _currentGrowthStage = _currentGrowthStage.Grow(_spriteRenderer, _plantConfig);
      
      if (Physics.Raycast(transform.position, Vector3.down, out var hit))
      {
         if (hit.collider.gameObject.CompareTag("Ground"))
         {
            var worldPos = hit.point;
            worldPos.y = hit.collider.bounds.max.y + _renderer.bounds.size.y / 2; 
            transform.position = worldPos;
            gameObject.transform.position = worldPos;
         }
      }
   }

   public abstract void CheckPlace();
   
   public abstract void Loot();

   private void OnDisable()
   {
      DayNightCycle.NextDay -= Grow;
   }
}
