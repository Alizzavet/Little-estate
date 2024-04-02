using UnityEngine;
using Pool;

public class TreeSpawner : PlantSpawner
{
   [SerializeField] private PlantConfig _birchConfig;
   [SerializeField] private PlantConfig _oakConfig;

   private PlantPreview _plantPreview;

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.B))
      {
         var plantPreview = PoolObject.Get<PlantPreview>();
         _plantPreview = plantPreview;
         _plantPreview.SetPlantSprite(_birchConfig.MatureStageConfig.Sprite);
      }

      if (Input.GetMouseButtonDown(0))
      {
         SpawnBirch(_plantPreview.transform.position);
      }
   }

   private void SpawnBirch(Vector3 position)
   {
      var plant = SpawnPlant(_birchConfig);
      plant.transform.position = position;
   }

   private void SpawnOak()
   {
      SpawnPlant(_oakConfig);
   }
}
