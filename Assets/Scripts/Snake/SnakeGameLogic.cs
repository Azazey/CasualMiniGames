using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGameLogic : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _componentsToDisable = new List<MonoBehaviour>();
    [SerializeField] private GameObject _looseWindow;

    public int Score;
    
    public event Action OnScoreChange;

    public void AddScore()
    {
        Score++;
        OnScoreChange?.Invoke();
    }
    
    public void GameOver()
    {
        foreach (var component in _componentsToDisable)
        {
            component.enabled = false;
        }
        _looseWindow.SetActive(true);
        Debug.Log("Game Over");
    }
}
