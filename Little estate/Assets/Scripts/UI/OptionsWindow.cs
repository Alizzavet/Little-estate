using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsWindow : Window
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;
    
    public AudioMixerGroup MixerVolume;

    private const string MusicMixer = "MusicVolume";
    private const string SoundMixer = "SoundVolume";

    private const float Duration = 0.4f;
    private void Awake()
    {
        _exitButton.onClick.AddListener(Close);
        
        _musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        _soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
    }
    private void OnEnable()
    {
        MixerVolume.audioMixer.GetFloat("MusicVolume", out var musicVolume);
        MixerVolume.audioMixer.GetFloat("SoundVolume", out var soundVolume);
        _musicSlider.value = musicVolume;
        _soundSlider.value = soundVolume;
    }

    private void OnMusicSliderValueChanged(float value)
    {
        MixerVolume.audioMixer.SetFloat(MusicMixer, value);
    }

    private void OnSoundSliderValueChanged(float value)
    {
        MixerVolume.audioMixer.SetFloat(SoundMixer, value);
    }
    private void Close()
    {
        transform.DOScale(Vector3.zero, Duration).SetAutoKill(true).OnComplete(WindowManager.CloseLast)
            .SetAutoKill(true);
    }
    private void OnDestroy()
    {
        _exitButton.onClick.RemoveListener(Close);
        _musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
        _soundSlider.onValueChanged.RemoveListener(OnSoundSliderValueChanged);
    }
}
