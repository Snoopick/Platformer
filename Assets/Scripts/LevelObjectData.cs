using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Objects/LevelObject", order = 1)]
public class LevelObjectData : ScriptableObject
{
    public string Name;
    public bool Static;
    public int Health;
}
