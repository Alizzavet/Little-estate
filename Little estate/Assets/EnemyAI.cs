using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyTest _enemyTest;


    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMoveController>();

        if (player != null)
        {
            _enemyTest.SetPlayer(player.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerMoveController>();

        if (player != null)
        {
            _enemyTest.UnsetPlayer();
        }
    }
}