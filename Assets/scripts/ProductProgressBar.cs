using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ProductProgressBar : MonoBehaviour
{
    private bool hasProgressCompleted;

    // public TMP_Text title;

    private Transform progressValue;
    private float stepSize;
    [SerializeField] private float stepRate = 0.02f;
    GameObject progressBar;


    void Awake()
    {
        // title = GameObject.Find("ProgressCard").transform.Find("name").GetComponent<TMP_Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        progressBar = GameObject.Find("ProgressBar");
        progressValue = progressBar.transform.Find("value");
        // value.GetComponent<RectTransform>().localScale = new Vector3(2.0f + stepSize, 1.0f, 1.0f);
        hasProgressCompleted = false;
        StartCoroutine(IncreaseProgress());
    }

    // Update is called once per frame
    void Update()
    {
        if (hasProgressCompleted)
        {
            StopCoroutine(IncreaseProgress());
        }
    }

    IEnumerator IncreaseProgress()
    {
        while (!hasProgressCompleted)
        {
            float currentVal = progressValue.GetComponent<RectTransform>().localScale.x;

            double totalDevSkills = Controller.instance.getCurrentTotalDevSkills();
            // double totalDesignSkills = 0;
            float DevStepSize = (float) totalDevSkills/(float) Controller.instance.getCurrentLevelProductDevSkillsTarget()* (float) Time.deltaTime;
            progressValue.GetComponent<RectTransform>().localScale = new Vector3(currentVal + DevStepSize, 1.0f, 1.0f);
            yield return new WaitForSeconds(stepRate);
            if (currentVal >= 0.9f)
            {
                hasProgressCompleted = true;
            }
        }
    }

    // public void DestroyProgressPanel()
    // {
    //     Destroy(gameObject);
    // }

    public bool HasProgressCompleted()
    {
        return hasProgressCompleted;
    }
}
