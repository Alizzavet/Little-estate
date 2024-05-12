using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour, IInputable
{
    private IInteractable _interactableObj;
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        _text.gameObject.SetActive(false);
        InputSystem.Instance.SetInput(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            _interactableObj = other.GetComponent<IInteractable>();
            _text.gameObject.SetActive(true);
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (_interactableObj == other.GetComponent<IInteractable>())
        {
            _interactableObj = null;
            _text.gameObject.SetActive(false);
        }
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
            _interactableObj?.Interact();
    }
}
