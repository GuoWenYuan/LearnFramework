using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFramework.Core;

public class GameEnter : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SGameEntry.Awake();
        SGameEntry.Log.Debug("Hello,World");
        SGameEntry.Log.Info("Hello,World");
        SGameEntry.Log.Warning("Hello,World");
        SGameEntry.Log.Error("Hello,World");
    }

    private void Start()
    {
        SGameEntry.Start();
    }

    // Update is called once per frame
    void Update()
    {
        SGameEntry.Update(Time.deltaTime,Time.realtimeSinceStartup);
    }

    private void OnDestroy()
    {
        SGameEntry.ShutDown();
    }
}


