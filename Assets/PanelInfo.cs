using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelInfo : MonoBehaviour
{
    private TMP_Text panel1Text;
    private TMP_Text panel2Text;
    // GameObject panel2;

    // void Awake()
    // {
    //     // panel1 = GameObject.Find("panel_1");
    //     // panel2 = GameObject.Find("panel_2");
    //     Debug.Log(panel1);
    // }
    // // Start is called before the first frame update
    void Start()
    {

        // panel1Text = GameObject.Find("panel_1").transform.Find("value").GetComponent<TMP_Text>();

        // Debug.Log("D: " + GameObject.Find("panel_1").transform.GetChild(1));
        Debug.Log("A:" + GameData.Instance.days.ToString());
        Debug.Log("B:" + GameData.Instance.revenue.ToString());

        panel1Text = GameObject.Find("panel_1").transform.Find("value").GetComponent<TMP_Text>();
        panel1Text.text = GameData.Instance.days.ToString();

        panel2Text = GameObject.Find("panel_1").transform.GetChild(1).GetComponent<TMP_Text>();
        panel2Text.text = ((int)GameData.Instance.revenue).ToString();
        // panel1.transform.Find("value").GetComponent<TMP_Text>().text = Controller.instance.daysSinceStart.ToString();
        // panel2.transform.Find("value").GetComponent<TMP_Text>().text = Controller.instance.revenue.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
