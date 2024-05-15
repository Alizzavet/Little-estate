using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Pool;

public class InteractMenu : MonoBehaviour, IInputable
{
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _content;

    public static InteractMenu Instance { get; private set; }
    
    private List<GameObject> _interactMenuItems = new();
    private int _currentIndex;
    private bool _isCreate;
    private PlantConfig _plantConfig;
    private Plant _plant;
    private Transform _plantTransform;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void Plant(PlantConfig config, Plant plant)
    {
        _plantConfig = config;
        _plant = plant;
    }

    public void GetTransform(Transform plant)
    {
        transform.position = plant.localPosition;
        _plantTransform = plant;
    }
    

    private void OnEnable()
    {
        InputSystem.Instance.SetTimedInput(this);
        PlayerInteract.Instance.ControlText(false);
        
        if(!_isCreate)
            CreateItems();
        
        UpdateItemSelection();
    }

    private void CreateItems()
    {
        // Получаем все типы, которые реализуют интерфейс IInteractMenuItem
        var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(IInteractMenuItem)) && t.IsClass);

        foreach (var type in types)
        {
            var instance = Instantiate(_itemPrefab);
            var addedScript = (IInteractMenuItem)instance.AddComponent(type);
            _interactMenuItems.Add(instance);

            var interactMenuItem = instance.GetComponent<InteractMenuItem>();
            interactMenuItem.GetTittle(addedScript.GetText());
            interactMenuItem.GetScript();
            
            instance.transform.SetParent(_content, false);
        }

        _isCreate = true;
    }
    
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Release();
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentIndex--;
            if (_currentIndex < 0)
                _currentIndex = _interactMenuItems.Count - 1;
            
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _currentIndex++;
            if (_currentIndex >= _interactMenuItems.Count)
                _currentIndex = 0;
            
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
        {
            var interactMenuItem = _interactMenuItems[_currentIndex].GetComponent<InteractMenuItem>();
            interactMenuItem.GetPlantConfig(_plantConfig, _plant, _plantTransform);
            interactMenuItem.Done();
        }

        UpdateItemSelection();
    }

    public void Release()
    {
        PlayerInteract.Instance.RemoveInteractObject();
        PoolObject.Release(this);
    }

    private void UpdateItemSelection()
    {
        for (var i = 0; i < _interactMenuItems.Count; i++)
        {
            var interactMenuItem = _interactMenuItems[i].GetComponent<InteractMenuItem>();
            if (i == _currentIndex)
                interactMenuItem.SetTextColor(Color.green);
            else
                interactMenuItem.SetTextColor(Color.white);
        }
    }

    private void OnDisable()
    {
        InputSystem.Instance.ReturnInput();
    }
}
