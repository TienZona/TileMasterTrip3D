using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text heartText;
    [SerializeField] TMP_Text starText;
    [SerializeField] TMP_Text coinText;
    [SerializeField] TextAsset playerJSON;

    void Start()
    {
     
        updateData();
    }


    private void updateData()
    {
        loadFilePlayerJSON();
       if(playerData != null)
        {
            levelText.text = string.Format("{0}", playerData.level);
            heartText.text = string.Format("{0}", playerData.heart);
            starText.text = string.Format("{0}", playerData.score);
            coinText.text = string.Format("{0}", playerData.coin);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(Scenes.Game.ToString());

    }

    public void loadFilePlayerJSON()
    {
        string path = Application.persistentDataPath + "/player.json";
        if (System.IO.File.Exists(path))
        {
            LoadFilePlayerData(path);
        }
        else
        {
            CreateFilePlayerData(path);
            LoadFilePlayerData(path);

        }


    }


    private void CreateFilePlayerData(string path)
    {
        PlayerData data = new PlayerData();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    private void LoadFilePlayerData(string path)
    {
        string json = File.ReadAllText(path);
        playerData = JsonUtility.FromJson<PlayerData>(json);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
