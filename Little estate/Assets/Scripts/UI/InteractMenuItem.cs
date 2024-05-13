using UnityEngine;
using TMPro;

public class InteractMenuItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _tittleText;

    public void GetTittle(string text)
    {
        _tittleText.text = text;
    }
    
    public void SetTextColor(Color color)
    {
        _tittleText.color = color;
    }

    public void Done()
    {
        var menuItem = GetComponent<IInteractMenuItem>();
        if (menuItem != null)
            menuItem.Execute();
        
    }
}
