using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    private void Awake(){
        instance = this;
        productCard = GameObject.Find("ProductCard");
        updateProductCardName();
    }

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
    public InstantiateProgress instantiateProgress;
    [SerializeField] public Button buildProductBtn;

    private GameObject productCard;

    private static int LEVEL_INCREMENT = 1;

    Dictionary<int, string> PRODUCT_LEVEL_MAPPING = new Dictionary<int, string>(){
        {
            1, "LANDING PAGE"
        },
        {
            2, "IMAGE ANNOTATIONS"
        },
        {
            3, "VIDEO ANNOTATIONS"
        },
        {
            4, "SFV1 ANNOTATIONS"
        },
        {
            5, "SFV2 ANNOTATIONS"
        },
        {
            6, "PCS ANNOTATIONS"
        }
    };
          

    [System.Serializable] public class TeamMember
    {
        public string Name;
        public double DevSkills;
        public double DesignSkills;
        public double Salary;

        public TeamMember(string SetName, double SetDevSkills, double SetDesignSkills, double SetSalary)
        {
            Name = SetName;
            DevSkills = SetDevSkills;
            DesignSkills = SetDesignSkills;
            Salary = SetSalary;
        }
    }

    public List<TeamMember> teamMembers;

    [System.Serializable] public class Candidate
    {
        public string Name;
        public double DevSkills;
        public double DesignSkills;
        public double Salary;
        public int UnlockLevel;

        public Candidate(string SetName, double SetDevSkills, double SetDesignSkills, double SetSalary, int SetUnlockLevel)
        {
            Name = SetName;
            DevSkills = SetDevSkills;
            DesignSkills = SetDesignSkills;
            Salary = SetSalary;
            UnlockLevel = SetUnlockLevel;
        }
    }
    public List<Candidate> candidates;

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

    private float EMPLOYEE_SALARY_FREQUENCY = 1;

    // Start is called before the first frame update
    public void Start()
    {
        cash = 220;
        brandValue = 0;
        revenue = 0;
        annotatorsCount = 2;
        GameLevel = 0;
        AnnotatorSalary = 10;

        teamMembers = new List<TeamMember>();
        teamMembers.Add(new TeamMember("Eve Wozniak", 15, 2, 100));
        teamMembers.Add(new TeamMember("Zuckberg", 12, 2, 90));
        TeamManager.instance.RenderTeamMembers();

        revenueMilestones = new List<RevenueMilestone>();
        revenueMilestones.Add(new RevenueMilestone(1, 500));
        revenueMilestones.Add(new RevenueMilestone(2, 1000));
        revenueMilestones.Add(new RevenueMilestone(3, 1500));
        revenueMilestones.Add(new RevenueMilestone(4, 2000));
        revenueMilestones.Add(new RevenueMilestone(5, 4000));

        allProjects = new List<Project>();
        allProjects.Add(new Project("First", 100, 0));
        allProjects.Add(new Project("Second", 200, 0));
        allProjects.Add(new Project("Third", 300, 1));
        allProjects.Add(new Project("Fourth", 500, 1));

        MarketProjects.instance.RenderProjects();
        
        InvokeRepeating("deductSalaries", EMPLOYEE_SALARY_FREQUENCY, EMPLOYEE_SALARY_FREQUENCY);

        candidates = new List<Candidate>();
        candidates.Add(new Candidate("Howard Wolowitz", 15, 5, 120, 1));
        candidates.Add(new Candidate("Eve Jobs", 1, 25, 120, 1));
        candidates.Add(new Candidate("Charles Babbage", 15, 5, 120, 1));
        candidates.Add(new Candidate("Barney Babbage", 15, 5, 120, 2));
        candidates.Add(new Candidate("Barney Sabotage", 15, 5, 120, 2));

        CandidateManager.instance.RenderCandidates();
        
    }

    // Update is called once per frame
    public void Update()
    {
        cashText.text = cash.ToString();
        brandValueText.text = brandValue.ToString();
        revenueText.text = revenue.ToString();
        annotatorsCountText.text = annotatorsCount.ToString();
        GameLevelText.text = (GameLevel + 1).ToString();

        daysSinceStart = DayCounter.Instance.getDays();
        daysSinceStartText.text = daysSinceStart.ToString();
        checkRevenueMilestones();
    }

    public List<TeamMember> getTeamMembers()
    {
        return teamMembers;
    }

    public void HireCandidate(Candidate candidate)
    {
        teamMembers.Add(new TeamMember(candidate.Name, candidate.DevSkills, candidate.DesignSkills, candidate.Salary));
        TeamManager.instance.RenderTeamMembers();
        candidates.Remove(candidate);
        CandidateManager.instance.RenderCandidates();
        Debug.Log("Candidate Hired: " + candidate.Name);
    }

    public void increaseRevenue(int increment)
    {
        revenue += increment;
        cash += increment;
    }

    public void increaseLevel(int increment)
    {
        GameLevel += increment;
        MarketProjects.instance.RenderProjects();
        CandidateManager.instance.RenderCandidates();
    }

    private void deductSalaries() 
    {
        double totalMonthlySalary = 0;
        totalMonthlySalary += annotatorsCount * AnnotatorSalary;
        foreach (TeamMember teamMember in teamMembers)
        {
            totalMonthlySalary += teamMember.Salary;
        }
        cash -= totalMonthlySalary/30;

        if (cash < 0)
        {
            SceneManager.LoadScene("Level_1");
        }
    }

    private void checkRevenueMilestones()
    {
        if(revenue >= revenueMilestones[GameLevel].Milestone)
        {
            levelUp();
        }
    }

    public void levelUp()
    {
        GameLevel += LEVEL_INCREMENT;
        MarketProjects.instance.RenderProjects();
        CandidateManager.instance.RenderCandidates();
        // instantiateProgress.UpdateButtonState(PRODUCT_LEVEL_MAPPING[GameLevel]);
        updateProductCardName();
    }

    private void updateProductCardName() {
        productCard.transform.Find("name").GetComponent<TMP_Text>().text = PRODUCT_LEVEL_MAPPING[GameLevel + 1];
        productCard.transform.Find("Button").GetComponent<Button>().interactable = true;
    }

    public string getCurrentLevelProductName() {
        return PRODUCT_LEVEL_MAPPING[GameLevel + 1];
    }
}
