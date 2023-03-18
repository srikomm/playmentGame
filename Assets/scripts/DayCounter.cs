using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DayCounter : MonoBehaviour
{

    // [SerializeField] private TMP_Text dayText;
    private TMP_Text dayText;
    float timer;
    int days;

    public static DayCounter Instance { get; private set; }

    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        dayText = GameObject.Find("InfoPanel_Days").transform.Find("value").GetComponent<TMP_Text>();
        // dayText.text = "asdasd";
        timer = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer - minutes * 60);

        // int seconds = timer % 60;
        // Debug.Log(seconds);
        // days = (int) seconds / 5;
        days = (int)(minutes * 60 + seconds)/2;
        dayText.text = days.ToString();
    }

    public int getDays()
    {
        return days;
    }
}
