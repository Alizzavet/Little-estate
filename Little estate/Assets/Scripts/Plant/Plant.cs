using UnityEngine;
using Pool;

public abstract class Plant : MonoBehaviour, IInteractable
{
   private PlantConfig _plantConfig;
   private SpriteRenderer _spriteRenderer;
   private GrowthStage _currentGrowthStage;
   private Renderer _renderer;
   private BoxCollider _collider;
   private Dryness _dryness;

   private void Awake()
   {
      _spriteRenderer = GetComponent<SpriteRenderer>();
      _renderer = GetComponent<Renderer>();
      _collider = GetComponent<BoxCollider>();
      _dryness = GetComponent<Dryness>();
   }

   private void OnEnable()
   {
      DayNightCycle.NextDay += Grow;
   }

   public void SetConfig(PlantConfig newConfig)
   {
      _plantConfig = newConfig;
      var seedling = _plantConfig;
      _spriteRenderer.sprite = seedling.Sprite;
      CheckCollider();
   }

   public Renderer GetPlantRenderer()
   {
      return _renderer;
   }

   private void Grow()
   {
      if (_currentGrowthStage == null)
      {
         _currentGrowthStage = new SeedlingStage();
         _spriteRenderer.sprite = _plantConfig.SeedlingStageConfig.Sprite;
      }
      else 
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
      
      CheckCollider();
   }

   private void CheckCollider()
   {
      var sprite = _spriteRenderer.sprite;
      var size = _collider.size;
      size = sprite.bounds.size;
      var zSize = 20f; 
      size = new Vector3(size.x, size.y, zSize);
      _collider.size = size;
      _collider.center = sprite.bounds.center;
   }
   
   public abstract void Loot();

   private void OnDisable()
   {
      DayNightCycle.NextDay -= Grow;
   }

   public void Interact()
   {
      if (_dryness.PlantDry)
      {
         Debug.Log("тратим энергию на полив!");
         _dryness.Moisten();
      }
      else if(_currentGrowthStage is MatureStage && !_dryness.PlantDry)
      {
         var menu = PoolObject.Get<InteractMenu>(); 
         menu.Plant(_plantConfig, this); 
         menu.GetTransform(gameObject.transform);
      }
   }
}
