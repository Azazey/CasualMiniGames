using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Animator _gameList;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _exitButton;
    
    private static readonly int Opened = Animator.StringToHash("Opened");
    private static readonly int Closed = Animator.StringToHash("Closed");

    private void Awake()
    {
        _playButton.onClick.AddListener(OpenGameList);
        _closeButton.onClick.AddListener(CloseGameList);
        _exitButton.onClick.AddListener(CloseApp);
    }

    private void OpenGameList()
    {
        _gameList.SetTrigger(Opened);
    }

    private void CloseGameList()
    {
        _gameList.SetTrigger(Closed);
    }

    private void CloseApp()
    {
        Application.Quit();
    }
}
