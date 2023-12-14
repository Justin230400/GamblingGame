using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Slot", menuName = "Gambling Game/Slot Machine/Slot")]
public class Slot : ScriptableObject
{
    public Sprite sprite;
    public int Weights;
    public int bettingOdds;
}
