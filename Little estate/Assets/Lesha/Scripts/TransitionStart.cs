using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class TransitionStart : Page
{
    public override void Show()
    {
        gameObject.SetActive(true);
        

        StartCoroutine(Animation());
    }


    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(2);
        PageManager.ClosePage();
    }
}
