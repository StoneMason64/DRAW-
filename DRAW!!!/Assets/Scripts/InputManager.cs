using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public UnityEvent onLeftClick;
    public UnityEvent onRightClick;

    public UnityEvent onTiggerPressed;

    private InputAction leftClick;
    private InputAction rightClick;

    public InputAction fireInput;

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

        Cursor.lockState = CursorLockMode.Locked;

        leftClick = new InputAction(binding: "<Mouse>/leftButton");
        leftClick.performed += ctx => onLeftClick.Invoke();
        leftClick.Enable();

        rightClick = new InputAction(binding: "<Mouse>/rightButton");
        rightClick.performed += ctx => onRightClick.Invoke();

        fireInput.performed += ctx => onTiggerPressed.Invoke();
        fireInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
