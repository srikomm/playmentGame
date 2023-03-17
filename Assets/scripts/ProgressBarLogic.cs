using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarLogic : MonoBehaviour
{
    private Slider slider;

    private float targetProgress;
    public float fillSpeed = 0.5f;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0;
        // IncrementProgress(.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < targetProgress) {
            slider.value += fillSpeed * Time.deltaTime;
        }   
    }

    public void IncrementProgress(float newProgress){
        targetProgress = slider.value + newProgress;
    }
}
