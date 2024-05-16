using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class EndRoom : MonoBehaviour, IInteractable
{
    [SerializeField] public Transform _endSpawnPos;
    

    public void Interact()
    {
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        PageManager.OpenPage<TransitionEnd>();
        yield return new WaitForSeconds(2);
        
        Debug.Log("Переход в новую локацию");
    }
}
