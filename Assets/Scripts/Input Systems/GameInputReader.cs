using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum GameInputActionMap
{
    Gameplay,
    GameMenu
}

public class GameInputReader : MonoBehaviour, GameInput.IGameMenuActions, GameInput.IGameplayActions
{
    private GameInput _gameInput;
    public static event UnityAction<Vector2> GameplayMoved;
    public static event UnityAction GameplayPaused;
    public static event UnityAction<Vector2> GameMenuNavigated;
    public static event UnityAction GameMenuBacked;
    public static event UnityAction GameMenuConfirmed;

    [SerializeField] private GameInputActionMap _currentActionMap;

    private void Awake()
    {
        _gameInput = new();
        _gameInput.Enable();
        switch (_currentActionMap)
        {
            case GameInputActionMap.Gameplay:
                SwitchToGameplay();
                break;
            case GameInputActionMap.GameMenu:
                SwitchToGameMenu();
                break;
        }
        
    }

    private void OnEnable()
    {
        GameStateController.StateSwitched += OnSwitchGameState;
        _gameInput.GameMenu.SetCallbacks(this);
        _gameInput.Gameplay.SetCallbacks(this);
    }

    private void OnDisable()
    {
        GameStateController.StateSwitched -= OnSwitchGameState;
        _gameInput.GameMenu.SetCallbacks(null);
        _gameInput.Gameplay.SetCallbacks(null);
    }

    private void OnSwitchGameState(GameState swichedState)
    {
        switch (swichedState)
        {
            case GameState.Play:
                SwitchToGameplay();
                break;
            default:
                SwitchToGameMenu();
                break;
        }
        Debug.Log(_currentActionMap.ToString());
    }
    private void SwitchToGameplay()
    {
        _gameInput.Disable();
        _gameInput.Gameplay.Enable();
        _currentActionMap = GameInputActionMap.Gameplay;
    }

    private void SwitchToGameMenu()
    {
        _gameInput.Disable();
        _gameInput.GameMenu.Enable();
        _currentActionMap = GameInputActionMap.GameMenu;
    }

    void GameInput.IGameMenuActions.OnNavigate(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("Navigated!");
                GameMenuNavigated?.Invoke(context.ReadValue<Vector2>());
                break;
            case InputActionPhase.Canceled:
                GameMenuNavigated?.Invoke(Vector2.zero);
                break;
        }
    }

    void GameInput.IGameMenuActions.OnBack(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Debug.Log("Backed!");
                GameMenuBacked?.Invoke();
                break;
        }
    }

    void GameInput.IGameMenuActions.OnConfirm(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Canceled:
                Debug.Log("Confirmed!");
                GameMenuConfirmed?.Invoke();
                break;
        }
    }

    void GameInput.IGameplayActions.OnMove(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("Moved");
                GameplayMoved?.Invoke(context.ReadValue<Vector2>());
                break;
            case InputActionPhase.Canceled:
                GameplayMoved?.Invoke(Vector2.zero);
                break;
        }
    }

    void GameInput.IGameplayActions.OnPause(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Debug.Log("Paused!");
                GameplayPaused?.Invoke();
                break;
        }
    }
}



