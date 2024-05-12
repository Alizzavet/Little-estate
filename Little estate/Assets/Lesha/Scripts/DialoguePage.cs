using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePage : Page, IInputable
{
    [SerializeField] private TMP_Text _text;

    private DialogueConfig _dialogue;
    private int _currentPhraseIndex = 0;

    public void SetText(DialogueConfig text)
    {
        _dialogue = text;
        _currentPhraseIndex = 0; 
        StartCoroutine(TypeSentence(_dialogue.AllPhrases[_currentPhraseIndex])); 
    }

    public void ChangeInput()
    {
        InputSystem.Instance.SetTimedInput(this);
    }

    public void HandleInput()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        _currentPhraseIndex++; 

        if (_currentPhraseIndex < _dialogue.AllPhrases.Count) 
        {
            StopAllCoroutines(); 
            StartCoroutine(TypeSentence(_dialogue.AllPhrases[_currentPhraseIndex])); 
        }
        else 
        {
            InputSystem.Instance.ReturnInput();
            PageManager.ClosePage();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        _text.text = "";
        foreach (char letter in sentence)
        {
            _text.text += letter;
            yield return null; 
        }
    }
}


