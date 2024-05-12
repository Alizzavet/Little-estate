public class DamagableObject : Enemy
{
    public override void OnDeath()
    {
        SpawnItems();
    }
}