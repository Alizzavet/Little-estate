using UnityEngine;

public class DestroyMenuItem : MonoBehaviour, IInteractMenuItem
{
    public string GetText()
    {
        return "Отправить в компост";
    }

    public void Execute()
    {
        Debug.Log("Выполняется действие 'Уничтожить'");
    }
}
