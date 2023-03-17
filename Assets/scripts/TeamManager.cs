using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamManager : MonoBehaviour
{
    public static TeamManager instance;
    private void Awake() => instance = this;

    List<Controller.TeamMember> teamMembers;

    public void RenderTeamMembers()
    {
        teamMembers = Controller.instance.getTeamMembers();

        GameObject buttonTemplate = this.transform.GetChild (1).gameObject;
        Debug.Log("ChildCount: " + this.transform.childCount);

        for (int i=2; i< this.transform.childCount; i++) 
        {
            Destroy (this.transform.GetChild (i).gameObject);
        }

        for (int i=0; i < teamMembers.ToArray().Length; i++){
            Debug.Log("TM" + i + " " + teamMembers[i].Name);
            GameObject g = (GameObject) Instantiate (buttonTemplate, this.transform);
            g.transform.GetChild (1).GetComponent <TextMeshProUGUI> ().text = teamMembers[i].Name;
            g.transform.GetChild (2).GetComponent <TextMeshProUGUI> ().text = teamMembers[i].DevSkills.ToString() + " / " + teamMembers[i].DesignSkills.ToString();
            g.SetActive(true);
        }

        Destroy (buttonTemplate);
    }
}
