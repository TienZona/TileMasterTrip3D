using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Data : MonoBehaviour
{
    public PlayerData loadFilePlayerJSON()
    {
        PlayerData playerData;
        string json = File.ReadAllText(Application.dataPath + "/data/player.json");

        playerData = JsonUtility.FromJson<PlayerData>(json);

        Debug.Log(playerData.level);
        return playerData;
    }
}
