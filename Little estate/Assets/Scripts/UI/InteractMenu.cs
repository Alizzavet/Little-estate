using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Pool;
using UnityEngine;

public class InteractMenu : MonoBehaviour, IInputable
{
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _content;

    public static InteractMenu Instance { get; private set; }
    
    private List<GameObject> _interactMenuItems = new();
    private int _currentIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void GetTransform(Transform plant)
    {
        transform.position = plant.localPosition;
    }

    private void OnEnable()
    {
        InputSystem.Instance.SetTimedInput(this);
        
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
            GameObject instance = Instantiate(_itemPrefab);
            IInteractMenuItem addedScript = (IInteractMenuItem)instance.AddComponent(type);
            _interactMenuItems.Add(instance);

            // Получаем компонент InteractMenuItem
            var interactMenuItem = instance.GetComponent<InteractMenuItem>();

            // Устанавливаем текст
            interactMenuItem.GetTittle(addedScript.GetText());

            // Устанавливаем этот объект в качестве родителя для префаба
            instance.transform.SetParent(_content, false);
        }
    }
    
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PoolObject.Release(this);
        
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
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            var interactMenuItem = _interactMenuItems[_currentIndex].GetComponent<InteractMenuItem>();
            interactMenuItem.Done();
        }

        UpdateItemSelection();
    }

    private void UpdateItemSelection()
    {
        for (int i = 0; i < _interactMenuItems.Count; i++)
        {
            var interactMenuItem = _interactMenuItems[i].GetComponent<InteractMenuItem>();
            if (i == _currentIndex)
            {
                interactMenuItem.SetTextColor(Color.green);
            }
            else
            {
                interactMenuItem.SetTextColor(Color.white);
            }
        }
    }

    private void OnDisable()
    {
        InputSystem.Instance.ReturnInput();
    }
}
