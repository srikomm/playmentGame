using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamManager : MonoBehaviour
{

    List<Controller.TeamMember> teamMembers;

    // Start is called before the first frame update
    public void Start()
    {
        teamMembers = Controller.instance.getTeamMembers();
    }

    // Update is called once per frame
    public void Update()
    {
        teamMembers = Controller.instance.getTeamMembers();

        // This is buggy. Creates a new component every frame.
        GameObject buttonTemplate = this.transform.GetChild (1).gameObject;

        for (int i=0; i < 1; i++){
            GameObject g = (GameObject) Instantiate (buttonTemplate, this.transform);
            g.transform.GetChild (1).GetComponent <TextMeshProUGUI> ().text = teamMembers[i].Name;
            g.transform.GetChild (2).GetComponent <TextMeshProUGUI> ().text = teamMembers[i].DevSkills.ToString() + " / " + teamMembers[i].DesignSkills.ToString();
        }

        Destroy (buttonTemplate);
    }
}
