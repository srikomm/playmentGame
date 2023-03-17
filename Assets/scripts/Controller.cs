// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    private void Awake() => instance = this;

    public double cash;
    public TMP_Text cashText;
    public double brandValue;
    public TMP_Text brandValueText;
    public double revenue;
    public TMP_Text revenueText;
    public double annotatorsCount;
    public TMP_Text annotatorsCountText;
    public double GameLevel;
    public TMP_Text GameLevelText;
    public double AnnotatorSalary;

    [System.Serializable] public class TeamMember
    {
        public string Name;
        public double DevSkills;
        public double DesignSkills;
        public double Salary;

        public TeamMember(string SetName, double SetDevSkills, int SetDesignSkills, double SetSalary)
        {
            Name = SetName;
            DevSkills = SetDevSkills;
            DesignSkills = SetDesignSkills;
            Salary = SetSalary;
        }
    }

    List<TeamMember> teamMembers;
    int daysSinceStart;
    public TMP_Text daysSinceStartText;

    // Start is called before the first frame update
    public void Start()
    {
        cash = 1000;
        brandValue = 0;
        revenue = 0;
        annotatorsCount = 2;
        GameLevel = 0;
        AnnotatorSalary = 10;
        
        teamMembers = new List<TeamMember>();
        teamMembers.Add(new TeamMember("Dev 1", 15, 2, 100));
        // teamMembers.Add(new TeamMember("Dev 2", 12, 2, 90));
        
    }

    // Update is called once per frame
    public void Update()
    {
        cashText.text = cash.ToString();
        brandValueText.text = brandValue.ToString();
        revenueText.text = revenue.ToString();
        annotatorsCountText.text = annotatorsCount.ToString();
        GameLevelText.text = GameLevel.ToString();

        daysSinceStart = DayCounter.Instance.getDays();
        daysSinceStartText.text = daysSinceStart.ToString();
        
    }

    public List<TeamMember> getTeamMembers()
    {
        return teamMembers;
    }
}
