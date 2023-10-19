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
    void Start()
    {
     
        updateData();
    }


    private void updateData()
    {
        loadFilePlayerJSON();
        levelText.text = string.Format("{0}", playerData.level);
        heartText.text = string.Format("{0}", playerData.heart);
        starText.text = string.Format("{0}", playerData.score);
        coinText.text = string.Format("{0}", playerData.coin);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(Scenes.Game.ToString());

    }

    public void loadFilePlayerJSON()
    {
        string json = File.ReadAllText(Application.dataPath + "/data/player.json");

        playerData = JsonUtility.FromJson<PlayerData>(json);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
