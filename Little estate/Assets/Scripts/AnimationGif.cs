using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationGif : MonoBehaviour
{
    [SerializeField] private List<Texture2D> _frames;

    private const float FramesPerSecond = 5f;
    private RawImage _rawImage;
    private Renderer _renderer;

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        var index = Time.time * FramesPerSecond;
        index = index % _frames.Count;

        if (_renderer)
            _renderer.material.mainTexture = _frames[(int) index];
        else 
            _rawImage.texture = _frames[(int) index];
    }
}
