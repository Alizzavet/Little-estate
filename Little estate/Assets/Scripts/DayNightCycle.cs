using System;
using UnityEngine;

public class DayNightCycle : MonoBehaviour, IInteractable
{
    public static event Action NextDay;

    private void SkipDay()
    {
        NextDay?.Invoke();
        Debug.Log("Вызвано событие на скип дня");
    }

    public void Interact()
    {
        SkipDay();
    }
}
