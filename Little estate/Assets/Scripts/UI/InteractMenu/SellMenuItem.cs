using UnityEngine;

public class SellMenuItem : MonoBehaviour, IInteractMenuItem
{
    public string GetText()
    {
        return "Продать";
    }

    public void Execute()
    {
        Debug.Log("Выполняется действие 'Продать'");
    }
}
