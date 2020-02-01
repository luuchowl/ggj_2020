using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data")]
public class AttackData : ScriptableObject
{
    public int id;
    public int damage;
    public int sweetSpotdamage;
}
