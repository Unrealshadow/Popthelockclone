using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMover : MonoBehaviour
{
    public DataHolder dataHolder;
    public Transform lockBase;

    [Space] public Direction direction;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private void Start()
    {
        _initialPosition = transform.localPosition;
        _initialRotation = transform.localRotation;
        
        EventManager.instance.AddListener<PaddleResetEvent>(ResetPaddlePosition);
    }

    private void OnDestroy()
    {
        EventManager.instance.RemoveListener<PaddleResetEvent>(ResetPaddlePosition);
    }

    private void Update()
    {
        if (dataHolder.gameState == GameState.Playing && Input.GetMouseButtonDown(0))
        {
            ChangeDirection();
        }
        
        if(dataHolder.gameState == GameState.Playing)
        {
            transform.RotateAround(lockBase.position, Vector3.forward, Time.deltaTime * dataHolder.currentPaddleSpeed * -(int)direction);
        }
    }

    private void ChangeDirection()
    {
        switch (direction)
        {
            case Direction.Clockwise:
                direction = Direction.AntiClockwise;
                break;
            case Direction.AntiClockwise:
                direction = Direction.Clockwise;
                break;
        }
    }

    private void ResetPaddlePosition(object obj)
    {
        transform.localPosition = _initialPosition;
        transform.localRotation = _initialRotation;
    }
}

public enum Direction
{
    Clockwise = 1,
    AntiClockwise = -1
}