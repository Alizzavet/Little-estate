using UI;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPage : Page
{
    [SerializeField] private Button _playButton;    
    [SerializeField] private Button _optionsButton;    
    [SerializeField] private Button _creatorsButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _optionsButton.onClick.AddListener(OpenOptionsWindow);
    }

    private void OpenOptionsWindow()
    {
        WindowManager.OpenWindow<OptionsWindow>();
    }

    private void OnDestroy()
    {
        _optionsButton.onClick.RemoveListener(OpenOptionsWindow);
    }
}
