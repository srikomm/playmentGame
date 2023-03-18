// using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.IO;

public static class ButtonExtension
{
    public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener (delegate() {
            OnClick(param);
        });
    }
}
public class CandidateManager : MonoBehaviour
{
    public static CandidateManager instance;
    private void Awake() => instance = this;

    public void RenderCandidates()
    {
        GameObject buttonTemplate = this.transform.GetChild (0).gameObject;

        for (int i=1; i< this.transform.childCount; i++) 
        {
            Destroy (this.transform.GetChild (i).gameObject);
        }

        for (int i=0; i < Controller.instance.candidates.ToArray().Length; i++){
            
            
            GameObject g = (GameObject) Instantiate (buttonTemplate, this.transform);

            g.transform.GetChild (0).transform.GetChild (1).GetComponent <TextMeshProUGUI> ().text = Controller.instance.candidates[i].Name;
            g.transform.GetChild (0).transform.GetChild (2).GetComponent <TextMeshProUGUI> ().text = "Dev: " + Controller.instance.candidates[i].DevSkills.ToString();
            g.transform.GetChild (0).transform.GetChild (3).GetComponent <TextMeshProUGUI> ().text = "Design: " + Controller.instance.candidates[i].DesignSkills.ToString();
            g.transform.GetChild (0).transform.GetChild (6).GetComponent <TextMeshProUGUI> ().text = "Salary: " + Controller.instance.candidates[i].Salary.ToString();
            
            if (Controller.instance.candidates[i].UnlockLevel > Controller.instance.GameLevel)
            {
                g.SetActive(false);
            }
            else
            {
                g.SetActive(true);
            }

            g.transform.GetChild (0).transform.GetChild (7).GetComponent <Button> ().AddEventListener (i, ItemClicked);
        
        }

        Destroy (buttonTemplate);
    }

    void ItemClicked (int ItemIndex)
    {
        Controller.instance.HireCandidate(Controller.instance.candidates[ItemIndex]);
    }

}
