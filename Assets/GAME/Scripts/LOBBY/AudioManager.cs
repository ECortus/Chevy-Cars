using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource[] toPlay;
    
    void Awake()
    {
        Init();
        Play(0);
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
        foreach (var VARIABLE in toPlay)
        {
            VARIABLE.Stop();
        }
        
        toPlay[i].Play();
    }
}
