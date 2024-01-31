using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "PS", menuName = "Player Settings")]
public class GamePlayerSettings : ScriptableObject
{
    public AudioMixer Mixer;
    public int ReviveAttempt = 1;
}
