using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamManager : MonoBehaviour
{
    public static TeamManager instance;
    private void Awake() => instance = this;

    List<Controller.TeamMember> teamMembers;

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void RenderTeamMembers()
    {
        teamMembers = Controller.instance.getTeamMembers();

        GameObject buttonTemplate = this.transform.GetChild (1).gameObject;

        for (int i=0; i < teamMembers.ToArray().Length; i++){
            GameObject g = (GameObject) Instantiate (buttonTemplate, this.transform);
            g.transform.GetChild (1).GetComponent <TextMeshProUGUI> ().text = teamMembers[i].Name;
            g.transform.GetChild (2).GetComponent <TextMeshProUGUI> ().text = teamMembers[i].DevSkills.ToString() + " / " + teamMembers[i].DesignSkills.ToString();
        }

        Destroy (buttonTemplate);
    }
}
