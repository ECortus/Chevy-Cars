using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PS", menuName = "Player Settings")]
public class PlayerSettings : ScriptableObject
{
    public uint ReviveAttempt = 1;
}
