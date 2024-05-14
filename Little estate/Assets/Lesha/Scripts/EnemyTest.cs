using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour, IDamagable
{
    private int _health = 10;

    [SerializeField] private NavMeshAgent _meshAgent;
    [SerializeField] private Collider _collider;
    
    private Transform _player;

    public void SetPlayer(Transform player)
    {
        _player = player;
    }
    public void UnsetPlayer()
    {
        _player = null;
    }
    
    public void TakeDamage(int damageCount)
    {
        _health -= damageCount;

        if (_health <= 0)
            Destroy(gameObject);

    }

    private void Update()
    {
        /*if (_player == null)
            _meshAgent.destination = transform.position;
        
        
        if(_player != null && _meshAgent.destination != _player.transform.position + Vector3.one)
            _meshAgent.destination = _player.transform.position;*/
    }
}
