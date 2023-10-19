using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBar : MonoBehaviour
{
    public GameObject Bar;
    public int time;

    void Start()
    {
        Bar.transform.localScale = Vector3.one;
    }
    public void StartTimeBar()
    {
        LeanTween.scaleX(Bar, 0, time);
    }
}
