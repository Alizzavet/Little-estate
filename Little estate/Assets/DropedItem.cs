using System;
using System.Collections;
using DG.Tweening;
using Pool;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropedItem : MonoBehaviour, ITakeable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public int Count { get; private set; }
    
    
    private Transform _startPos;

    public DropedItemConfig DropedItemConfig;

    public bool CanTaking { get; private set; }
    
    private IEnumerator IdleAnimation()
    {
        var randomX = Random.Range(-3f, 3f);
        var randomY = -1f;
        var randomZ = Random.Range(-3f, 3f);

        transform.position = _startPos.position;
        var newPosition = transform.position + new Vector3(randomX, randomY, randomZ);
        transform.DOJump(newPosition, 3, 1, 1).SetEase(Ease.Flash).SetAutoKill(true);
        
        yield return new WaitForSeconds(1f);
        CanTaking = true;
        transform.position = newPosition;
        
        // вверх-вниз
        var duration = 1f; 
        var distance = 1f; 
        var upPosition = newPosition + new Vector3(0, distance, 0); 
        var downPosition = newPosition; 

        while (true) 
        {
            yield return transform.DOMove(upPosition, duration).SetEase(Ease.InOutSine).WaitForCompletion();
            yield return transform.DOMove(downPosition, duration).SetEase(Ease.InOutSine).WaitForCompletion();
        }
    }
    
    public DropedItemConfig Take()
    {
        _startPos = null;
        PoolObject.Release(this);
        CanTaking = false;
        return DropedItemConfig;
    }

    public void SetData(DropedItemConfig config, Transform enemyPos)
    {
        DropedItemConfig = config;
        _spriteRenderer.sprite = config.Sprite;
        Count = config.CountToSpawn;
        
        _startPos = enemyPos;
        StartCoroutine(IdleAnimation());
    }
    

    public void SetDropData(DropedItemConfig config, Transform playerPos)
    {
        CanTaking = false;
        DropedItemConfig = config;
        _spriteRenderer.sprite = config.Sprite;
        Count = config.Count;
        
        var randomX = Random.Range(-3f, 3f);
        var randomY = -1f;
        var randomZ = Random.Range(-3f, 3f);
        
        var newPosition = playerPos.position + new Vector3(randomX, randomY, randomZ);
        
        transform.position = newPosition;

        StartCoroutine(DropedAnimation());

    }
    private IEnumerator DropedAnimation()
    {
        yield return new WaitForSeconds(2f);
        CanTaking = true;
        
        // вверх-вниз
        var duration = 1f; 
        var distance = 1f; 
        var upPosition = transform.position + new Vector3(0, distance, 0); 
        var downPosition = transform.position; 

        while (true) 
        {
            yield return transform.DOMove(upPosition, duration).SetEase(Ease.InOutSine).WaitForCompletion();
            yield return transform.DOMove(downPosition, duration).SetEase(Ease.InOutSine).WaitForCompletion();
        }
    }


}
