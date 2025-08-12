using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUIController : MonoBehaviour
{
    //[SerializeField] GameStateController gameStateController;
    [SerializeField] GameObject _pauseMenu;

    private void OnEnable()
    {
        GameStateController.StateSwitched += OnSwitchGameState;
        GameInputReader.GameMenuNavigated += OnNavigateMenu;
    }
    private void OnDisable()
    {
        GameStateController.StateSwitched -= OnSwitchGameState;
        GameInputReader.GameMenuNavigated -= OnNavigateMenu;
    }
    private void EnablePauseMenu(bool isEnable)
    {
        _pauseMenu.SetActive(isEnable);
    }


    private void OnBack()
    {

    }
    private void OnNavigateMenu(Vector2 navigation)
    {
        Debug.Log($"navigation: {navigation}");
    }

    private void OnSwitchGameState(GameState switchedState)
    {
        switch (switchedState)
        {
            case GameState.Pause:
                EnablePauseMenu(true);
                break;
            case GameState.Play:
                EnablePauseMenu(false);
                break;
        }
    }
}
