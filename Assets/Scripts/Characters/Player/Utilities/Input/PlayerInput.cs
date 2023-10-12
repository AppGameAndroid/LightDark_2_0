using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }
    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

    private void Awake()
    {
        InputActions = new PlayerInputActions();
        PlayerActions = InputActions.Player;
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }
    private void OnDisable()
    {
        InputActions.Disable();
    }

    public void DisableActionFor(InputAction action, float seconds)
    {
        StartCoroutine(DisablableAction(action, seconds));
        //startCorutine solo se pueden usar en Monobehavior. 
    }

    private IEnumerator DisablableAction(InputAction action, float seconds)
    {
        action.Disable();
        yield return new WaitForSeconds(seconds);
        action.Enable();
    }
}
