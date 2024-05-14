using UnityEngine;
using TMPro;

public class InteractMenuItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _tittleText;

    private IInteractMenuItem _iInteractMenuItem;

    public void GetScript()
    {
        _iInteractMenuItem = GetComponent<IInteractMenuItem>();
    }

    public void GetPlantConfig(PlantConfig plantConfig, Plant plant, Transform transform)
    {
        _iInteractMenuItem.GetConfig(plantConfig, plant, transform);
    }

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
        if (_iInteractMenuItem != null)
            _iInteractMenuItem.Execute();
        
        InteractMenu.Instance.Release();
    }
}
