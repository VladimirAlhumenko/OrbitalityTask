using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _startNewGameButton;

    [SerializeField]
    private Button _loadGameButton;

    [SerializeField]
    private Button _exit;

    private void OnEnable()
    {
        _startNewGameButton.onClick.AddListener(StartGame);
        _loadGameButton.onClick.AddListener(Load);
        _exit.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        _startNewGameButton.onClick.RemoveListener(StartGame);
        _loadGameButton.onClick.RemoveListener(Load);
        _exit.onClick.RemoveListener(Exit);
    }

    private void StartGame()
    {
        SceneHelper.LoadScene(Scenes.Game);
    }

    private void Load()
    {
        
    }

    private void Exit()
    {
       
    }
}
