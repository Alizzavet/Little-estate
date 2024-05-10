using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pool;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageText : MonoBehaviour, IReleasable
{
    [SerializeField] private TMP_Text _damageText;


    public void SetText(int damageCount)
    {
        _damageText.text = damageCount.ToString();
        var randomX = Random.Range(-2f, 2f);
        var randomY = Random.Range(-1f, 1f);
        var randomZ = Random.Range(-1f, 1f);

        transform.DOJump(transform.position + new Vector3(randomX, randomY, randomZ), 3, 1, 1);
    }

    private void OnEnable()
    {
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(1);
        _damageText.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        PoolObject.Release(this);
    }

    public void OnRelease()
    {
        StopCoroutine(Animation());
        _damageText.DOFade(1, 0);
    }
}