using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum Direction
{
    Forward,
    Left,
    Right,
    Back
}

public class Snake : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private Direction _currentDirection;
    [SerializeField] private GameObject _bodyPart;
    [SerializeField] private int _gap;

    [SerializeField] private SnakeGameLogic _snakeGameLogic;

    public List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionHistory = new List<Vector3>();

    public GameObject GrowTail()
    {
        GameObject newTail = Instantiate(_bodyPart, transform.position + new Vector3(0f,-2f,0f), Quaternion.identity);
        BodyParts.Add(newTail);
        return newTail;
    }

    public void OnSnackEaten()
    {
        _snakeGameLogic.AddScore();
    }

    private void Update()
    {
        PositionHistory.Insert(0, transform.position);

        if (Input.GetKey(KeyCode.W))
        {
            if (_currentDirection != Direction.Back)
            {
                _currentDirection = Direction.Forward;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (_currentDirection != Direction.Forward)
            {
                _currentDirection = Direction.Back;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (_currentDirection != Direction.Right)
            {
                _currentDirection = Direction.Left;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (_currentDirection != Direction.Left)
            {
                _currentDirection = Direction.Right;
            }
        }

        int index = 1;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionHistory[Mathf.Min(index * _gap, PositionHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * _speed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
    }

    private void FixedUpdate()
    {
        MoveForward();
        Rotate();
    }

    private void Rotate()
    {
        switch (_currentDirection)
        {
            case Direction.Forward:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.Back:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
        }
    }

    private void MoveForward()
    {
        switch (_currentDirection)
        {
            case Direction.Forward:
                _rigidbody.velocity = new Vector3(0, 0, _speed);
                break;
            case Direction.Back:
                _rigidbody.velocity = new Vector3(0, 0, -_speed);
                break;
            case Direction.Left:
                _rigidbody.velocity = new Vector3(-_speed, 0, 0);
                break;
            case Direction.Right:
                _rigidbody.velocity = new Vector3(_speed, 0, 0);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            _snakeGameLogic.GameOver();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Tail"))
        {
            _snakeGameLogic.GameOver();
        }
    }
}