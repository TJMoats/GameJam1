using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class InputEvent : UnityEvent<InputEventData>
{
}

public class InputManager : MonoBehaviour
{
    static private InputManager _instance;

    public InputEvent OnDPadEvent;
    public InputEvent OnButtonEvent;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogWarning("WARNING: InputManager already exists and this will not be referenced.");
        }
    }

    public void FixedUpdate()
    {
        // Get inputs
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Dictionary<InputKeys, InputKeyState> keyState = GetKeyStates();
        
        // Navigation and Movement
        OnDPadEvent.Invoke(new InputEventData() { DPadDirection = direction });

        // Button Inputs
        OnButtonEvent.Invoke(new InputEventData()
        {
            DPadDirection = direction,
            KeyStateMap = keyState,
        });
    }

    private Dictionary<InputKeys, InputKeyState> GetKeyStates()
    {
        Dictionary<InputKeys, InputKeyState> keyStates = new Dictionary<InputKeys, InputKeyState>();
        foreach(InputKeys keyName in Enum.GetValues(typeof(InputKeys)))
        {
            string buttonName = Enum.GetName(typeof(InputKeys), keyName);
            if (Input.GetButtonDown(buttonName))
            {
                keyStates[keyName] = InputKeyState.DownThisFrame;
            }
            else if (Input.GetButtonUp(buttonName))
            {
                keyStates[keyName] = InputKeyState.UpThisFrame;
            }
            else if (Input.GetButton(buttonName))
            {
                keyStates[keyName] = InputKeyState.Held;
            }
            else
            {
                keyStates[keyName] = InputKeyState.Released;
            }
        }

        return keyStates;
    }
}

public enum InputKeys
{
    BasicAction,
    SecondaryAction,
    ShieldAction,
    InteractionAction,
    MenuAction,
}

public enum InputKeyState
{
    Released,
    DownThisFrame,
    Held,
    UpThisFrame,
}

public class InputEventData
{
    public bool Handled = false;
    public Vector2 DPadDirection;
    public Dictionary<InputKeys, InputKeyState> KeyStateMap = new Dictionary<InputKeys, InputKeyState>();
}