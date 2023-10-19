using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level")]
public class Level : ScriptableObject
{
    public int level;
    public string MapName;
    public string DisplayName;
    public int PlayTime;

    public List<GameObject> ListTile = new List<GameObject>();
}