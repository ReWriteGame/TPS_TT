using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class HeroInput : MonoBehaviour, IInitializable
{
    [SerializeField] private Hero hero;

    private UserInput input;

    private void Awake() => Initialize();
    private void OnDestroy() => Unsubscribe();
    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();
    private void Update() => Move();

    public void Initialize()
    {
        input = new UserInput();
        Subscribe();
    }

    private void Subscribe()
    {
        input.Player.Jump.performed += Jump;
        input.Player.Sprint.performed += Sprint;
        input.Player.Look.performed += Look;
        input.Player.Fire.performed += Shoot;
        input.Player.ItemSelect.performed += ScrollMouse;
    }

    private void Unsubscribe()
    {
        input.Player.Jump.performed -= Jump;
        input.Player.Sprint.performed -= Sprint;
        input.Player.Look.performed -= Look;
        input.Player.Fire.performed -= Shoot;
        input.Player.ItemSelect.performed -= ScrollMouse;
    }

    private void Move()
    {
        hero.inputUserDirection = input.Player.Move.ReadValue<Vector2>();
    }

    private void Jump(InputAction.CallbackContext callback)
    {
        hero.Character.Jump = callback.ReadValueAsButton();
    }

    private void Sprint(InputAction.CallbackContext callback)
    {
        hero.Sprint = Convert.ToBoolean(callback.ReadValue<float>());
    }

    private void Look(InputAction.CallbackContext callback)
    {
        // playerView.look = callback.ReadValue<Vector2>();
    }

    private void Shoot(InputAction.CallbackContext callback)
    {
        hero.FireAction();
    }

    private void ScrollMouse(InputAction.CallbackContext callback)
    {
        bool scrollUp = callback.ReadValue<Vector2>().y < 0;
        if (scrollUp) hero.SelectNextItem();
        else hero.SelectPreviousItem();
    }
}
