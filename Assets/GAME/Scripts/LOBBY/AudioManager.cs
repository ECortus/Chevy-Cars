using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private static AudioSource[] toPlay;
    
    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        Init();
        // Play(0);
    }

    void Init()
    {
        DontDestroyOnLoad(gameObject);

        foreach (Transform VARIABLE in transform)
        {
            DontDestroyOnLoad(VARIABLE.gameObject);
        }

        toPlay = GetComponentsInChildren<AudioSource>();
    }

    public static void Play(int i)
    {
        Stop();
        toPlay[i].Play();
    }
    
    public static void Stop()
    {
        foreach (var VARIABLE in toPlay)
        {
            VARIABLE.Stop();
        }
    }
}
