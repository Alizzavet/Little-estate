using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pool;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropedItem : MonoBehaviour, ITakeable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private int _count;
    private string _id;
    
    private Transform _startPos;

    private DropedItemConfig _dropedItemConfig;
    
    private IEnumerator Animation()
    {
        var randomX = Random.Range(-3f, 3f);
        var randomY = -1f;
        var randomZ = Random.Range(-3f, 3f);

        transform.position = _startPos.position;
        var newPosition = transform.position + new Vector3(randomX, randomY, randomZ);
        transform.DOJump(newPosition, 3, 1, 1).SetEase(Ease.Flash).SetAutoKill(true);
        
        yield return new WaitForSeconds(1f);
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

    public void SetData(DropedItemConfig config, Transform enemyPos)
    {
        _dropedItemConfig = config;
        _spriteRenderer.sprite = config.Sprite;
        _count = config.Count;
        _id = config.Id;
        
        _startPos = enemyPos;
        StartCoroutine(Animation());
    }

    public DropedItemConfig Take()
    {
        _startPos = null;
        PoolObject.Release(this);
        return _dropedItemConfig;
    }
}
