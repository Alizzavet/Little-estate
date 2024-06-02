using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDungeon : MonoBehaviour, IInteractable
{
    public string SceneName;
    public void Interact()
    {
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        PageManager.OpenPage<TransitionEnd>();
        yield return new WaitForSeconds(2);
        
        Debug.Log("Переход в новую локацию");
        SceneManager.LoadScene(SceneName);
    }
}
