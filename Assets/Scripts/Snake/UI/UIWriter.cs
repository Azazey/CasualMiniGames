using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private SnakeGameLogic _snakeGameLogic;
    
    public void WriteScore()
    {
        if (_score)
        {
            _score.text = "Счёт:"+_snakeGameLogic.Score;
        }
    }

    private void Start()
    {
        WriteScore();
        _snakeGameLogic.OnScoreChange += WriteScore;
    }
}
