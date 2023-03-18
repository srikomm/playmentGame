using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeamManager : MonoBehaviour
{
    public static TeamManager instance;
    private void Awake() => instance = this;

    List<Controller.TeamMember> teamMembers;

    public void RenderTeamMembers()
    {
        teamMembers = Controller.instance.getTeamMembers();

        GameObject buttonTemplate = this.transform.GetChild (0).gameObject;

        for (int i=1; i< this.transform.childCount; i++) 
        {
            Destroy (this.transform.GetChild (i).gameObject);
        }

        for (int i=0; i < teamMembers.ToArray().Length; i++){
            Debug.Log("TM: " + teamMembers[i].Name);
            GameObject g = (GameObject) Instantiate (buttonTemplate, this.transform);

            Sprite sprite = Resources.Load ("Assets/avatars/Ellipse-" + i, typeof(Sprite)) as Sprite;
            
            g.transform.GetChild (1).GetComponent <Image> ().sprite = sprite;

            g.transform.GetChild (2).GetComponent <TextMeshProUGUI> ().text = teamMembers[i].Name;
            g.transform.GetChild (3).GetComponent <TextMeshProUGUI> ().text = teamMembers[i].DevSkills.ToString();
            g.transform.GetChild (4).GetComponent <TextMeshProUGUI> ().text = teamMembers[i].DesignSkills.ToString();
            g.SetActive(true);
        }

        Destroy (buttonTemplate);
    }
}
