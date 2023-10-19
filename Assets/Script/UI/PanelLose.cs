using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLose : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] LeanTweenType Type;
    [SerializeField] float time;
    
    public void open()
    {
        UI.transform.localPosition = new Vector3(0, 1000, 0);
        this.gameObject.SetActive(true);
        UI.LeanMoveLocal(Vector3.zero, time).setEase(Type);
        Invoke(nameof(pauseGame), 1);
    }

    public void openNotAnimation()
    {
        UI.transform.localPosition = new Vector3(0, 0, 0);
        this.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    private void pauseGame()
    {
        Time.timeScale = 0;
    }
    public void close()
    {
        UI.LeanMoveLocal(new Vector3(0, 1000, 0), time).setEase(Type);
        Invoke(nameof(hide), time);
    }

    public void closeNotAnimation()
    {
        gameObject.SetActive(false);
        Debug.Log(123);
    }

    public void hide()
    {
        this.gameObject.SetActive(false);
    }
}
