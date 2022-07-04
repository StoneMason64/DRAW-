using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public UnityEvent onLeftClick;
    public UnityEvent onRightClick;

    public UnityEvent onLeftTiggerPressed;
    public UnityEvent onLeftTiggerReleased;
    public UnityEvent onRightTiggerPressed;
    public UnityEvent onRightTiggerReleased;

    public UnityEvent onAButton;
    public UnityEvent onBButton;

    private InputAction leftClick;
    private InputAction rightClick;

    public InputAction fireInput;
    public InputAction fireRelease;
    public InputAction selectInput;
    public InputAction pauseInput;

    // Start is called before the first frame update
    void Start()
    {
        /*
        var mouse = Mouse.current;

        if (mouse != null)
        {
            Debug.Log("Mouse Has been detected.");
        }*/

        /*var keybaord = Keyboard.current;
        if(keybaord != null)
        {
            Debug.Log("Keyboard Detected");
        }*/

        //Cursor.lockState = CursorLockMode.Locked;

        // link input actions to unity events
        leftClick = new InputAction(binding: "<Mouse>/leftButton");
        leftClick.performed += action => onLeftClick.Invoke();
        leftClick.Enable();

        rightClick = new InputAction(binding: "<Mouse>/rightButton");
        rightClick.performed += action => onRightClick.Invoke();
        rightClick.Enable();

        fireInput.performed += action => onRightTiggerPressed.Invoke();
        fireInput.canceled += action => onRightTiggerReleased.Invoke();
        fireInput.Enable();

        selectInput.performed += action => onAButton.Invoke();
        selectInput.Enable();

        pauseInput.performed += action => onBButton.Invoke();
        pauseInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
