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
    public int GameLevel;
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
    public List<TeamMember> teamMembers;
    int daysSinceStart;
    public TMP_Text daysSinceStartText;

    [System.Serializable] public class RevenueMilestone
    {
        public int ToLevel;
        public double Milestone;

        public RevenueMilestone(int SetToLevel, double SetMilestone)
        {
            ToLevel = SetToLevel;
            Milestone = SetMilestone;
        }
    }
     public List<RevenueMilestone> revenueMilestones;

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

    public List<Project> allProjects;

    private float EMPLOYEE_SALARY_FREQUENCY = 3;

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
        teamMembers.Add(new TeamMember("Dev 2", 12, 2, 90));
        TeamManager.instance.RenderTeamMembers();

        revenueMilestones = new List<RevenueMilestone>();
        revenueMilestones.Add(new RevenueMilestone(1, 10000));
        revenueMilestones.Add(new RevenueMilestone(2, 100000));
        revenueMilestones.Add(new RevenueMilestone(3, 1000000));
        revenueMilestones.Add(new RevenueMilestone(4, 10000000));
        revenueMilestones.Add(new RevenueMilestone(5, 100000000));

        allProjects = new List<Project>();
        allProjects.Add(new Project("First", 500, 0));
        allProjects.Add(new Project("Second", 1000, 1));
        allProjects.Add(new Project("Third", 2000, 1));
        allProjects.Add(new Project("Fourth", 5000, 1));

        MarketProjects.instance.RenderProjects();
        
        InvokeRepeating("deductSalaries", EMPLOYEE_SALARY_FREQUENCY, EMPLOYEE_SALARY_FREQUENCY);
        
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

    public void increaseRevenue(int increment)
    {
        revenue += increment;
        cash += increment;

        if(revenue >= revenueMilestones[GameLevel].Milestone)
        {
            increaseLevel(1);
        }
    }

    public void increaseLevel(int increment)
    {
        GameLevel += increment;
        MarketProjects.instance.RenderProjects();

    }
    private void deductSalaries() 
    {
        double totalMonthlySalary = 0;
        totalMonthlySalary += annotatorsCount * AnnotatorSalary;
        foreach (TeamMember teamMember in teamMembers)
        {
            totalMonthlySalary += teamMember.Salary;
        }
        cash -= totalMonthlySalary;
    }
}
