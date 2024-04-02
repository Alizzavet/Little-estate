using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopWindow _shopWindow;
    [SerializeField] private Button _shopButton;

    private bool _isOnScene;

    private void Awake()
    {
        _shopButton.onClick.AddListener(OnShopButtonClicked);
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

    private void OnDestroy()
    {
        _shopButton.onClick.RemoveListener(OnShopButtonClicked);
    }
}
