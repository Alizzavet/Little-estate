using System.Collections.Generic;
using Pool;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopWindow _shopWindow;
    [SerializeField] private Button _shopButton;
    [SerializeField] private List<PlantConfig> _plantConfigs;
    [SerializeField] private Button _treeButton;
    [SerializeField] private GameObject _shopWindowContent;

    private bool _isOnScene;

    private void Awake()
    {
        _shopButton.onClick.AddListener(OnShopButtonClicked);
        _treeButton.onClick.AddListener(CreateTreeItems);
    }

    private void OnShopButtonClicked()
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
        foreach (var plantConfig in _plantConfigs)
        {
            var item = PoolObject.Get<ShopItemBar>();
            item.SetData(plantConfig);
            item.transform.SetParent(_shopWindowContent.transform, false);
        }
    }

    private void OnDestroy()
    {
        _shopButton.onClick.RemoveListener(OnShopButtonClicked);
        _treeButton.onClick.RemoveListener(CreateTreeItems);
    }
}
