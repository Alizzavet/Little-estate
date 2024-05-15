using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour, IInputable
{
    [SerializeField] private TMP_Text _text;
    
    public static PlayerInteract Instance { get; private set; }
    
    private IInteractable _interactableObj;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        ControlText(false);
        InputSystem.Instance.SetInput(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            _interactableObj = other.GetComponent<IInteractable>();
            ControlText(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (_interactableObj == other.GetComponent<IInteractable>())
            RemoveInteractObject();
        
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
            _interactableObj?.Interact();
    }

    public void RemoveInteractObject()
    {
        _interactableObj = null;
        ControlText(false);
    }

    public void ControlText(bool value)
    {
        _text.gameObject.SetActive(value);
    }
}
