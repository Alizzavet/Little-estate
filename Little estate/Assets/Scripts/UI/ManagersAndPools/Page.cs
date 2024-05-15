// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

namespace UI
{
    public class Page : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

