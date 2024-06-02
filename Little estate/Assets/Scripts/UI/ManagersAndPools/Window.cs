// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class Window : MonoBehaviour
    {
        private Vector3 _defaultScale;
        
        public void Show()
        {
            _defaultScale = transform.localScale;
            gameObject.SetActive(true);
            
            transform.localScale = Vector3.zero;

            transform.DOScale(_defaultScale, 0.6f).SetEase(Ease.OutFlash).SetAutoKill(true);

        }

        public void Hide()
        {
            transform.localScale = _defaultScale;
            gameObject.SetActive(false);
        }
    }
}
