using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DataHolder dataHolder;

    private void Awake()
    {
        dataHolder.ResetLevel();
    }

    
}
