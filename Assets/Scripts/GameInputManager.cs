using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour {

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    private PlayerInputActions playerInputActions;

    public static GameInputManager Instance { get; private set; }

    public event EventHandler OnAnyKeyAction;

    private bool isReversed = false;

    public enum Binding {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
    }

    private void Awake() {
        Instance = this;

        playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS)) {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        playerInputActions.Player.Enable();

        playerInputActions.Player.AnyKey.performed += AnyKey_performed;
    }

    private void OnDestroy() {
        playerInputActions.Dispose();
    }

    private void AnyKey_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnAnyKeyAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return isReversed ? -inputVector : inputVector;
    }

    public void SetReversedControls(bool newValue) {
        isReversed = newValue;
    }
}
