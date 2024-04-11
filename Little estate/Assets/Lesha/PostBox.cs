using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class PostBox : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueConfig _dia;
    
    public void Interact()
    {
        var a = PageManager.OpenPage<DialoguePage>();
        a.ChangeInput();
        a.SetText(_dia);
    }

    public string InteractableText()
    {
        return "Это почтовый ящик. ХМММММММММММММММММММММММ";
    }
}
