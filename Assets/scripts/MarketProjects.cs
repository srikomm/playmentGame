// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MarketProjects : MonoBehaviour
{
    public static MarketProjects instance;
    private void Awake() => instance = this;

    public void RenderProjects()
    {
        // allProjects = Controller.instance.getTeamMembers();

        GameObject buttonTemplate = this.transform.GetChild (0).gameObject;

        for (int i=0; i < Controller.instance.allProjects.ToArray().Length; i++){
            if (Controller.instance.allProjects[i].Level <= Controller.instance.GameLevel)
            {
                GameObject g = (GameObject) Instantiate (buttonTemplate, this.transform);
                g.transform.GetChild (1).GetComponent <TextMeshProUGUI> ().text = Controller.instance.allProjects[i].Name;
                g.transform.GetChild (3).GetComponent <TextMeshProUGUI> ().text = Controller.instance.allProjects[i].Revenue.ToString();
            }
        }

        Destroy (buttonTemplate);
    }
}
