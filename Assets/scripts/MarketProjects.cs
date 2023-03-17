// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MarketProjects : MonoBehaviour
{

    [Serializable] public class Project
    {
        public string Name;
        public double Revenue;

        public Project(string NewName, double NewRevenue)
        {
            Name = NewName;
            Revenue = NewRevenue;
        }
    }

    



    // Start is called before the first frame update
    void Start()
    {
        List<Project> allProjects = new List<Project>();
        allProjects.Add(new Project("First", 500));
        allProjects.Add(new Project("Second", 1000));
        

        GameObject buttonTemplate = this.transform.GetChild (0).gameObject;

        // int N = allProjects.;

        for (int i=0; i < 2; i++){
            GameObject g = (GameObject) Instantiate (buttonTemplate, this.transform);
            g.transform.GetChild (0).GetComponent <TextMeshProUGUI> ().text = allProjects[i].Name;
            g.transform.GetChild (1).GetComponent <TextMeshProUGUI> ().text = allProjects[i].Revenue.ToString(); 
        }

        Destroy (buttonTemplate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
