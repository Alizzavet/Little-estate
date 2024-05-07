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
        
        transform.position = _startPos.position;
        
        var randomX = Random.Range(-3f, 3f);
        var randomY = -1f;
        var randomZ = Random.Range(-3f, 3f);
        
        transform.DOJump(transform.position + new Vector3(randomX, randomY, randomZ), 3, 1, 1).SetEase(Ease.Flash).SetAutoKill(true);
        
        yield return new WaitForSeconds(1.5f);
        transform.DOJump(transform.position, 1, 1, 3).SetLoops(-1);
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
        PoolObject.Release(this);
        return _dropedItemConfig;
    }
}
