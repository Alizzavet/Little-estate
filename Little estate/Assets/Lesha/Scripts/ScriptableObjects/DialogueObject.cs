using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class DialogueObject : InteractableObject, IInteractable
{
    public void Interact()
    {
        var a = PageManager.OpenPage<DialoguePage>();
        a.ChangeInput();
        a.SetText(_dia);
    }
}
