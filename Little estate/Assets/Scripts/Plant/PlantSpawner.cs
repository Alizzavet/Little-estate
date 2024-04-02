using UnityEngine;
using Pool;

public abstract class PlantSpawner : MonoBehaviour
{
    protected Plant SpawnPlant(PlantConfig config)
    {
        var plant = PoolObject.Get<Plant>();
        plant.SetConfig(config);
        return plant;
    }
}
