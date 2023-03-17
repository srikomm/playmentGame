// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MarketProjects : MonoBehaviour
{

    [System.Serializable] public class Project
    {
        public string Name;
        public double Revenue;
        public int Level;

        public Project(string SetName, double SetRevenue, int SetLevel)
        {
            Name = SetName;
            Revenue = SetRevenue;
            Level = SetLevel;
        }
    }

    



    // Start is called before the first frame update
    void Start()
    {
        List<Project> allProjects = new List<Project>();
        allProjects.Add(new Project("First", 500, 0));
        allProjects.Add(new Project("Second", 1000, 0));
        allProjects.Add(new Project("Third", 2000, 1));
        allProjects.Add(new Project("Fourth", 5000, 1));
        

        GameObject buttonTemplate = this.transform.GetChild (2).gameObject;

        // int N = allProjects.;

        for (int i=0; i < 2; i++){
            GameObject g = (GameObject) Instantiate (buttonTemplate, this.transform);
            g.transform.GetChild (1).GetComponent <TextMeshProUGUI> ().text = allProjects[i].Name;
            g.transform.GetChild (4).GetComponent <TextMeshProUGUI> ().text = allProjects[i].Revenue.ToString();

            if (allProjects[i].Level > 0){
                g.SetActive(false);
            }
        }

        Destroy (buttonTemplate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
