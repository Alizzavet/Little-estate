using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pool;

public class ShopManager : MonoBehaviour, IInteractable
{
    [SerializeField] private ShopWindow _shopWindow;
    [SerializeField] private List<PlantConfig> _plantConfigs;
    [SerializeField] private Button _treeButton;
    [SerializeField] private Button _fruitPlantButton;
    [SerializeField] private Button _buildingButton;
    [SerializeField] private GameObject _shopWindowContent;

    private bool _isOnScene;
    private bool _itemsCreated;
    private List<ShopItemBar> _items = new();

    private void Awake()
    {
        _treeButton.onClick.AddListener(CreateTreeItems);
        _fruitPlantButton.onClick.AddListener(ClearItems);
        _buildingButton.onClick.AddListener(ClearItems);
    }

    private void Start()
    {
        _shopWindow.ToScene += SetBool;
    }

    public void Interact()
    {
        if (!_isOnScene)
        {
            _shopWindow.MoveToScene();
            _isOnScene = true;
        }
        else
        {
            _shopWindow.MoveToBack();
            _isOnScene = false;
        }
    }

    private void CreateTreeItems()
    {
        if (_itemsCreated) 
            return;
    
        foreach (var plantConfig in _plantConfigs)
        {
            var item = PoolObject.Get<ShopItemBar>();
            item.SetData(plantConfig);
            item.transform.SetParent(_shopWindowContent.transform, false);
            item.IsShop += _shopWindow.MoveToBack;
            _items.Add(item);
        }

        _itemsCreated = true;
    }

    private void ClearItems()
    {
        foreach (var item in _items)
        {
            item.IsShop -= _shopWindow.MoveToBack;
            Destroy(item.gameObject);
        }
        _items.Clear();
        _itemsCreated = false;
    }

    private void SetBool(bool value)
    {
        _isOnScene = value;
    }
    

    private void OnDestroy()
    {
        _treeButton.onClick.RemoveListener(CreateTreeItems);
        _fruitPlantButton.onClick.RemoveListener(ClearItems);
        _buildingButton.onClick.RemoveListener(ClearItems);
        
        _shopWindow.ToScene -= SetBool;
    }
}
