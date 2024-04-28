using System;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Button _skipDayButton;
    
    public static event Action NextDay;

    private void Awake()
    {
        _skipDayButton.onClick.AddListener(OnSkipDayButtonClicked);
    }

    private void OnSkipDayButtonClicked()
    {
        SkipDay();
    }
    
    private void SkipDay()
    {
        NextDay?.Invoke();
        Debug.Log("Вызвано событие на скип дня");
    }

    private void OnDestroy()
    {
        _skipDayButton.onClick.RemoveListener(OnSkipDayButtonClicked);
    }
}
