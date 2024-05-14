using System;
using Pool;
using UnityEngine;

public class Dryness : MonoBehaviour
{
    public Boolean PlantDry { get; private set; }

    private Plant _plant;
    private SpriteRenderer _spriteRenderer;
    private int _dayToDie;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _plant = GetComponent<Plant>();
    }

    private void OnEnable()
    {
        DayNightCycle.NextDay += Dry;

        _dayToDie = 0;
    }

    private void Dry()
    {
        PlantDry = true;
        _spriteRenderer.color = new Color(0.65f, 0.16f, 0.16f);
        _dayToDie++;
    }

    public void Moisten()
    {
        PlantDry = false;
        _spriteRenderer.color = Color.white;
        _dayToDie = 0;
    }

    private void Update()
    {
        if (_dayToDie == 2)
            PoolObject.Release(_plant);
    }

    private void OnDisable()
    {
        DayNightCycle.NextDay -= Dry;
    }
}
