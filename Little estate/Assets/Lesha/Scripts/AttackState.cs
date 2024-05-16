/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private bool isPunching;
    public AttackState(BatEnemy batEnemy)
    {
        this.batEnemy = batEnemy;
    }

    public override void Enter()
    {
        // Здесь можно добавить код, который будет выполняться при входе в состояние Attack
        isPunching = false;
    }

    public override void Execute()
    {
        // Здесь можно добавить код, который будет выполняться во время состояния Attack
        if (batEnemy.Player != null && !isPunching)
        {
            isPunching = true;
            batEnemy.StartCoroutine(PunchPlayer());
        }
    }

    public override void Exit()
    {
        // Здесь можно добавить код, который будет выполняться при выходе из состояния Attack
        batEnemy.StopCoroutine(PunchPlayer());
    }

    private IEnumerator PunchPlayer()
    {
        var playerpos = batEnemy.Player.position;
        var direction = (playerpos - batEnemy.transform.position).normalized;
        
        var remainingDistance = batEnemy.PunchDist;
        
        batEnemy.Line.SetPosition(0, batEnemy.transform.position); // начальная точка линии
        batEnemy.Line.SetPosition(1, playerpos); // конечная точка линии
        batEnemy.Line.enabled = true;
        
        
        yield return new WaitForSeconds(1);
        batEnemy.Line.enabled = false;

        while (remainingDistance > 0)
        {
            float distanceToMove = batEnemy.PunchSpeed * Time.deltaTime;
            batEnemy.CharacterController.Move(direction * distanceToMove);
            remainingDistance -= distanceToMove;
            yield return null;
        }
        
        yield return new WaitForSeconds(1);
        batEnemy.ChangeState(new PatrolState(batEnemy));
        isPunching = false;
    }
}
*/

