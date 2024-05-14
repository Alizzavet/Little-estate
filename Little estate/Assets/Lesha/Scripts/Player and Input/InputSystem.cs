 using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private List<IInputable> _currentInput = new();
    private List<IInputable> _previousInput = new();

    public static InputSystem Instance;
    
    public InputSystem()
    {
        _currentInput = new List<IInputable>();
        _previousInput = new List<IInputable>();
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        
    }

    public void SetInput(IInputable input)
    {
        _currentInput.Add(input);
    }

    public void SetTimedInput(IInputable input)
    {
        _previousInput.AddRange(_currentInput);
        
        ClearInput();
        _currentInput.Add(input);
    }
    
    public void ReturnInput()
    {

        ClearInput();
        _currentInput.AddRange(_previousInput);
        _previousInput.Clear();  
    }
    

    public void ClearInput()
    {
        _currentInput.Clear();
    }

    private void Update()
    {
        if (_currentInput.Count == 0)
            return;
        var tempInput = new List<IInputable>(_currentInput);
        
        foreach (var input in tempInput)
        {
            input?.HandleInput();
        }
    }
}