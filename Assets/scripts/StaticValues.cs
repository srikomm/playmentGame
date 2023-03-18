using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaticValues : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        double revenue = GameValues.revenue;
        double days = GameValues.days;
        // GameObject.Find("panel_1").transform.GetChild(1).GetComponent<TMP_Text>().text = days.ToString();
        // GameObject.Find("panel_2").transform.GetChild(1).GetComponent<TMP_Text>().text = revenue.ToString();

        GameObject.Find("panel_1").transform.GetChild(1).GetComponent<TMP_Text>().text = GameData.Instance.days.ToString();
        GameObject.Find("panel_2").transform.GetChild(1).GetComponent<TMP_Text>().text = GameData.Instance.revenue.ToString();
        
    }
}
