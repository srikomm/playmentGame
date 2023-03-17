using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseProgressbutton : MonoBehaviour
{

    private ProgressBarLogic pbl;
    public Button yourButton;

    private void Awake() {
        pbl = new ProgressBarLogic();
    }

    // Start is called before the first frame update
    void Start()
    {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(StartSlider);
        // yourButton.onClick.AddListener(StartSlider);
    }

    // Update is called once per frame
    void Update()
    {
        pbl.IncrementProgress(.75f);
    }

    public void StartSlider() {
        pbl.IncrementProgress(.75f);
    }
}
