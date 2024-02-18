using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private GameObject _currentDot;
    private GameObject _previousDot;
    private readonly float _loseThreshold = 0.5f;
    [Space] public DataHolder dataHolder;
    
    private void Start()
    {
        EventManager.instance.AddListener<DotHitEvent>(OnDotHit);
        EventManager.instance.AddListener<DotMissEvent>(OnDotMiss);
        EventManager.instance.AddListener<LevelWinEvent>(OnLevelWin);
    }

    private void OnDestroy()
    {
        EventManager.instance.RemoveListener<DotHitEvent>(OnDotHit);
        EventManager.instance.RemoveListener<DotMissEvent>(OnDotMiss);
        EventManager.instance.RemoveListener<LevelWinEvent>(OnLevelWin);
    }

    private void OnLevelWin(object obj)
    {
        dataHolder.StopGame();
        dataHolder.IncreaseSpeed(5);
    }

    private void OnDotMiss(object obj)
    {
        Debug.Log("Dot Miss");
        dataHolder.DecreaseSpeed(2);
        _previousDot = null;
        dataHolder.StopGame();
    }

    private void OnDotHit(object obj)
    {
        Debug.Log("Dot Hit");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _currentDot = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _previousDot = _currentDot;
        _currentDot = null;
    }

    private void Update()
    {
        if (dataHolder.gameState == GameState.Playing)
        {
            if (_previousDot && GetDistanceFromPreviousDot() > _loseThreshold)
            {
                // Raise the dot miss event
                EventManager.instance.TriggerEvent(new DotMissEvent());
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (_currentDot != null)
                {
                    if (_currentDot.GetComponent<Star>())
                    {
                        dataHolder.stars++;
                    }
                    dataHolder.dotsRemaining--;
                    Destroy(_currentDot);
                    
                    if (dataHolder.dotsRemaining <= 0)
                    {
                        dataHolder.dotsRemaining = 0;
                        dataHolder.currentLevel++;
                        // Raise the level win event
                        EventManager.instance.TriggerEvent(new LevelWinEvent());
                    }
                    else
                    {
                        // Raise the dot hit event
                        EventManager.instance.TriggerEvent(new DotHitEvent());
                    }
                }
                else
                {
                    // Raise the dot miss event
                    EventManager.instance.TriggerEvent(new DotMissEvent());
                }
            }
        }
    }

    private float GetDistanceFromPreviousDot()
    {
        return (transform.position - _previousDot.transform.position).magnitude;
    }
}
