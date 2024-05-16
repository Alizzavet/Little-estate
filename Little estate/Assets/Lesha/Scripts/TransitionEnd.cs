using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class TransitionEnd : Page
{
    public override void Show()
    {
        gameObject.SetActive(true);
    }
}
