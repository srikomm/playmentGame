// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

// public static class ButtonExtension
// {
//     public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
//     {
//         button.onClick.AddListener (delegate() {
//             OnClick(param);
//         });
//     }
// }

public class MarketProjects: MonoBehaviour {
    public static MarketProjects instance;
    private void Awake() => instance = this;

    public void RenderProjects() {
        // allProjects = Controller.instance.getTeamMembers();

        GameObject buttonTemplate = this.transform.GetChild(0).gameObject;

        for (int i=1; i< this.transform.childCount; i++)
        {
            Destroy (this.transform.GetChild (i).gameObject);
        }

        for (int i = 0; i < Controller.instance.allProjects.ToArray().Length; i++) {
            if ((Controller.instance.allProjects[i].Level <= Controller.instance.GameLevel) &&
                (Controller.instance.allProjects[i].ProjectState != Controller.ProjectState.COMPLETED)) {
                GameObject g = (GameObject) Instantiate(buttonTemplate, this.transform);
                g.transform.GetChild(1).GetComponent < TextMeshProUGUI > ().text = Controller.instance.allProjects[i].Name;
                g.transform.GetChild(3).GetComponent < TextMeshProUGUI > ().text = Controller.instance.allProjects[i].Revenue.ToString();
                g.transform.GetChild(5).GetComponent < TextMeshProUGUI > ().text = Controller.instance.allProjects[i].UnitsToComplete.ToString();
                
                g.transform.GetChild(7).GetComponent < TextMeshProUGUI > ().text = "Assigned Annotators: " + Controller.instance.allProjects[i].AssignedAnnotatorsToProject.ToString();

                g.transform.GetChild(6).GetComponent <Button> ().AddEventListener (i, ItemClicked);
                if (Controller.instance.isCurrentLevelFeatureBuilt) {
                    if( Controller.instance.allProjects[i].ProjectState == Controller.ProjectState.YET_TO_BE_PICKED_UP)
                    {
                        g.transform.GetChild(6).GetComponent <Button> ().interactable = true;
                    }
                    else if ( Controller.instance.allProjects[i].ProjectState == Controller.ProjectState.STARTED) {
                        g.transform.GetChild(6).GetComponent <Button> ().interactable = false;
                    }
                }
                else {
                    if ((Controller.instance.allProjects[i].Level <= Controller.instance.GameLevel) &&
                    ( Controller.instance.allProjects[i].ProjectState == Controller.ProjectState.YET_TO_BE_PICKED_UP))
                    {
                        g.transform.GetChild(6).GetComponent <Button> ().interactable = true;
                    }
                    else {
                        g.transform.GetChild(6).GetComponent <Button> ().interactable = false;
                    }
                }
                
            }
        }

        Destroy(buttonTemplate);
    }

    void ItemClicked(int ItemIndex) {
        Controller.instance.startProject(Controller.instance.allProjects[ItemIndex]);
    }

}