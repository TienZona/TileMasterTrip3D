using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    
    public GameObject queue;
    public List<Level> levels;
    public List<GameObject> prefabs;
    public GameObject comboBox;
    public GameObject timeBar;

    public PlayerData playerData;
    public LevelData levelData;

    public GameObject Bar;
    public int BarTime;
    public int number;
    public int numberTile = 0;
    public float minute = 10;
    private float second = 0;
    private bool timerIsRunning = false;
    private int score = 0;
    private int combo = 0;
    private int level = 0;
    

    private List<GameObject> tiles;

    public TMP_Text timeText;
    public TMP_Text scoreText;
    public TMP_Text levelText;
    public TMP_Text comboText;

    public GameObject PanelWin;
    public GameObject PanelLose;
    public GameObject PanelPause;

    public GameObject SpawnPoint;
    
    Coroutine coroutine;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        loadFilePlayerJSON();
        startGame();
        timerIsRunning = true;
        second = minute * 60;
        
    }

    void Update()
    {
        
        if (timerIsRunning)
        {
            if (second > 0)
            {
                second -= Time.deltaTime;
                DisplayTime(second);
            }
            else
            {
                Debug.Log("Time has run out!");
                second = 0;
                timerIsRunning = false;
            }
        }

       

    }

    public void startGame()
    {
        Time.timeScale = 1;
        setDataGame();
        SpawnPoint.SetActive(true);
        SpawnPoint.GetComponent<SpawnPoint>().Spawn();

        scoreText.text = string.Format("{0}", score);
        levelText.text = string.Format("Lv.{0}", level);
    }
    public void setDataGame()
    {
        if(playerData != null)
        {
            int level = playerData.level;
            loadFileLevelJSON(level);

            if(levelData != null)
            {
                this.level = levelData.level;
                number = levelData.number;
                minute = levelData.time;
                score = 0;
                second = minute * 60;
                combo = 0;

            }
        }

      
    }


    public void loseGame()
    {
        destroyAllTile();
        PanelLose.SetActive(true);
        playerData.heart -= 1;
        saveFilePlayerJSON();

    }

    public void winGame()
    {
        if (level < 3)
        {
            level += 1;
        }
        else
        {
            level = 1;
        }
        saveFilePlayerJSON();
    }

    public void pauseGame()
    {

    }

    public void continueGame()
    {
        Time.timeScale = 1;
        
    }

    public void handleScore()
    {
        
        handleCombo();
        score += combo;
        scoreText.text = string.Format("{0}", score);
    }

    public void handleCombo()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        comboBox.SetActive(true);
        Bar.transform.localScale = Vector3.one;
        coroutine = StartCoroutine(StartCombo(5));
        combo++;
        comboText.text = string.Format("Combo x {0}", combo);
    }
    IEnumerator StartCombo(float time)
    {
        float temp = time;
        float step = 100;
        while (time > 0)
        {
            yield return new WaitForSeconds(1 / step);
            time -= 1 / step;

            Bar.transform.localScale = new Vector3((time/temp), 1, 1);
        }

        if(time <= 0)
        {
            comboBox.SetActive(false);
            combo = 0;
            StopCoroutine(coroutine);
        }
    }

    public void backHome()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach(var tile in tiles) {
            Destroy(tile);
        }
        StopAllCoroutines();
        SceneManager.LoadScene(Scenes.Menu.ToString());
    }


    public void checkWinGame()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        if (tiles.Length == 0 && numberTile == 0)
        {
            PanelWin.SetActive(true);
            winGame();
            Debug.Log("Win Game");
        }
        
    }

    public void destroyAllTile()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            Destroy(tile);
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void loadFileLevelJSON(int level)
    {
        string json = File.ReadAllText(Application.dataPath + "/data/level_" + level + ".json");

        levelData = JsonUtility.FromJson<LevelData>(json);

    }

    public void loadFilePlayerJSON()
    {
        string json = File.ReadAllText(Application.dataPath + "/data/player.json" );

        playerData = JsonUtility.FromJson<PlayerData>(json);
    }
    public void saveFilePlayerJSON()
    {
        PlayerData data = new PlayerData();
        data.level = this.level;
        data.score = playerData.score + this.score;
        data.heart = playerData.heart;
        data.coin = playerData.coin;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/data/player.json", json);
    }
}
