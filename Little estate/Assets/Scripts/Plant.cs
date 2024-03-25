using UnityEngine;

public abstract class Plant : MonoBehaviour
{
   [SerializeField] private Collider2D _collider2D;
   [SerializeField] private Rigidbody2D _rigidbody2D;
   [SerializeField] private PlantConfig _plantConfig;

   public abstract void CheckPlace();

   public abstract void Grow();

   public abstract void Loot();
}
