using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Play,
    Pause,
    MainMenu
}
public class GameStateController : MonoBehaviour
{
    private static GameState _currentState;
    [SerializeField] private GameState _testedCurrentState;

    public static event UnityAction<GameState> StateSwitched;

    public void SwitchState(GameState switchedState)
    {
        if (_currentState != switchedState)
        {
            _currentState = switchedState;
        }
        StateSwitched?.Invoke(switchedState);
        Debug.Log($"Current game state: {switchedState}");
    }
    private void Start()
    {
        SwitchState(_testedCurrentState);
    }
    private void OnTestSwitchState(GameState switchedState)
    {
        if (_testedCurrentState != switchedState)
        {
            _testedCurrentState = switchedState;
        }
    }

    private void OnEnable()
    {
        GameInputReader.GameplayPaused += OnPressDownPause;
        StateSwitched += OnTestSwitchState;
    }
    private void OnDisable()
    {
        GameInputReader.GameplayPaused -= OnPressDownPause;
        StateSwitched -= OnTestSwitchState;
    }

    private void OnValidate()
    {
        if (_currentState != _testedCurrentState)
        {
            SwitchState(_testedCurrentState);
        }
    }

    private void OnPressDownPause()
    {
        SwitchToPauseState();
    }

    public void SwitchToPauseState()
    {
        SwitchState(GameState.Pause);
    }

    public void SwitchToPlayState()
    {
        SwitchState(GameState.Play);
    }
    public void SwitchToMainMenuState()
    {
        SwitchState(GameState.MainMenu);
    }
}
