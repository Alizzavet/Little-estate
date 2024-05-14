using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Coin Instance { get; private set; }

    public event Action<float> ChangeCurrency; 

    private float _coinCount = 6f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        if (ChangeCurrency != null)
            ChangeCurrency.Invoke(_coinCount); 
    }

    public void SpendCoin(float price)
    {
        if(price > _coinCount)
            return;
        
        _coinCount -= price;
        
        if (ChangeCurrency != null)
            ChangeCurrency.Invoke(_coinCount);
    }

    public void GetCoin(float count)
    {
        _coinCount += count;
        
        if (ChangeCurrency != null)
            ChangeCurrency.Invoke(_coinCount);
    }

    public float CurrentCoinCount()
    {
        return _coinCount;
    }
}
