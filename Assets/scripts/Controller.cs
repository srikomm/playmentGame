using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Controller: MonoBehaviour {
    public static Controller instance;
    private void Awake() {
        instance = this;
        productCard = GameObject.Find("ProductCard");
        updateProductCardNameAndButtonState();
        projectsPanel = GameObject.Find("ProjectsPanel");
    }

    public double cash;
    public TMP_Text cashText;
    public double projectsFinished;
    public TMP_Text projectsFinishedText;
    public double revenue;
    public TMP_Text revenueText;
    public double annotatorsCount;
    public TMP_Text annotatorsCountText;
    public double occupiedAnnotators;
    public TMP_Text occupiedAnnotatorsText;
    public double GameLevel;
    public TMP_Text GameLevelText;
    public double AnnotatorSalary;
    public InstantiateProductProgress instantiateProductProgress;
    [SerializeField] public Button buildProductBtn;
    public bool isCurrentLevelFeatureBuilt;

    private GameObject productCard;
    private GameObject projectsPanel;

    private static double LEVEL_INCREMENT = 1;

    Dictionary < double, string > PRODUCT_LEVEL_MAPPING = new Dictionary < double, string > () {
        {
            1,
            "LANDING PAGE"
        }, {
            2,
            "IMAGE ANNOTATIONS"
        }, {
            3,
            "VIDEO ANNOTATIONS"
        }, {
            4,
            "SFV1 ANNOTATIONS"
        }, {
            5,
            "SFV2 ANNOTATIONS"
        }, {
            6,
            "PCS ANNOTATIONS"
        }
    };

    Dictionary < double, double > PRODUCT_DEV_MAPPING = new Dictionary < double, double > () {
        {
            1,
            30
        }, {
            2,
            100
        }, {
            3,
            400
        }, {
            4,
            800
        }, {
            5,
            1600
        }, {
            6,
            3200
        }
    };

    Dictionary < double, double > PRODUCT_DESIGN_MAPPING = new Dictionary < double, double > () {
        {
            1,
            30
        }, {
            2,
            60
        }, {
            3,
            120
        }, {
            4,
            240
        }, {
            5,
            480
        }, {
            6,
            960
        }
    };

    [System.Serializable] public class TeamMember {
        public string Name;
        public double DevSkills;
        public double DesignSkills;
        public double Salary;

        public TeamMember(string SetName, double SetDevSkills, double SetDesignSkills, double SetSalary) {
            Name = SetName;
            DevSkills = SetDevSkills;
            DesignSkills = SetDesignSkills;
            Salary = SetSalary;
        }
    }

    public List < TeamMember > teamMembers;

    [System.Serializable] public class Candidate {
        public string Name;
        public double DevSkills;
        public double DesignSkills;
        public double Salary;
        public double UnlockLevel;

        public Candidate(string SetName, double SetDevSkills, double SetDesignSkills, double SetSalary, double SetUnlockLevel) {
            Name = SetName;
            DevSkills = SetDevSkills;
            DesignSkills = SetDesignSkills;
            Salary = SetSalary;
            UnlockLevel = SetUnlockLevel;
        }
    }
    public List < Candidate > candidates;

    public double daysSinceStart;
    public TMP_Text daysSinceStartText;

    [System.Serializable] public class RevenueMilestone {
        public double ToLevel;
        public double Milestone;

        public RevenueMilestone(double SetToLevel, double SetMilestone) {
            ToLevel = SetToLevel;
            Milestone = SetMilestone;
        }
    }

    public List < RevenueMilestone > revenueMilestones;

    public enum ProjectState {
        YET_TO_BE_PICKED_UP,
        STARTED,
        COMPLETED
    }

    [System.Serializable] public class Project {
        public string Name;
        public double Revenue;
        public double Level {
            get;
            set;
        }
        public double UnitsToComplete;
        public double StartDay {
            get;
            set;
        }
        public double AssignedAnnotatorsToProject {
            get;
            set;
        }
        public ProjectState ProjectState;
        public double PercentageDone {
            get;
            set;
        }

        public Project(string SetName, double SetRevenue, double SetLevel, double SetUnitsToComplete) {
            Name = SetName;
            Revenue = SetRevenue;
            Level = SetLevel;
            UnitsToComplete = SetUnitsToComplete;
            StartDay = -1;
            AssignedAnnotatorsToProject = 0;
            ProjectState = ProjectState.YET_TO_BE_PICKED_UP;
            PercentageDone = 0;
        }

        public double updateAndGetPercentage() {
            PercentageDone = (double)(((Controller.instance.daysSinceStart - StartDay) * AssignedAnnotatorsToProject) / UnitsToComplete);
            return PercentageDone;
        }
    }

    public bool startProject(Project project) {

        int numberOfAnnotators = int.Parse(projectsPanel.transform.GetChild(2).GetComponent < TMP_InputField > ().text);

        if ((numberOfAnnotators <= 0) || (Controller.instance.annotatorsCount == Controller.instance.occupiedAnnotators) || (project.Level > Controller.instance.GameLevel) || (isCurrentLevelFeatureBuilt == false)) {
            return false;
        }
        if (numberOfAnnotators > (Controller.instance.annotatorsCount - Controller.instance.occupiedAnnotators)) {
            project.AssignedAnnotatorsToProject = Controller.instance.annotatorsCount - Controller.instance.occupiedAnnotators;
            Controller.instance.occupiedAnnotators = Controller.instance.annotatorsCount;
        } else {
            project.AssignedAnnotatorsToProject = numberOfAnnotators;
            Controller.instance.occupiedAnnotators += numberOfAnnotators;
        }
        project.ProjectState = ProjectState.STARTED;
        project.StartDay = Controller.instance.daysSinceStart;
        MarketProjects.instance.RenderProjects();
        return true;
    }

    public void finishProject(Project project) {
        Debug.Log("Project is finishing: " + project.Name);
        Controller.instance.occupiedAnnotators -= project.AssignedAnnotatorsToProject;
        Controller.instance.cash += project.Revenue;
        Controller.instance.revenue += project.Revenue;
        project.AssignedAnnotatorsToProject = 0;
        project.ProjectState = ProjectState.COMPLETED;
        MarketProjects.instance.RenderProjects();
        projectsFinished += 1;
    }

    public List < Project > allProjects;

    private float EMPLOYEE_SALARY_FREQUENCY = 1;
    private float RENDER_FREQUENCY = 1;

    // Start is called before the first frame update
    public void Start() {
        cash = 490;
        projectsFinished = 0;
        revenue = 0;
        annotatorsCount = 20;
        occupiedAnnotators = 0;
        GameLevel = 0;
        AnnotatorSalary = 10;
        isCurrentLevelFeatureBuilt = false;

        teamMembers = new List < TeamMember > ();
        teamMembers.Add(new TeamMember("Eve Wozniak", 15, 2, 170));
        teamMembers.Add(new TeamMember("Zuckberg", 12, 2, 140));

        TeamManager.instance.RenderTeamMembers();

        revenueMilestones = new List < RevenueMilestone > ();
        revenueMilestones.Add(new RevenueMilestone(1, 500));
        revenueMilestones.Add(new RevenueMilestone(2, 1000));
        revenueMilestones.Add(new RevenueMilestone(3, 1500));
        revenueMilestones.Add(new RevenueMilestone(4, 2000));
        revenueMilestones.Add(new RevenueMilestone(5, 4000));

        allProjects = new List < Project > ();
        allProjects.Add(new Project("First", 100, 0, 50));
        allProjects.Add(new Project("Second", 200, 0, 100));
        allProjects.Add(new Project("Third", 300, 0, 150));
        allProjects.Add(new Project("Fourth", 200, 0, 400));
        allProjects.Add(new Project("Fifth", 300, 1, 700));
        allProjects.Add(new Project("Sixth", 400, 1, 1200));
        allProjects.Add(new Project("7th", 300, 1, 3000));
        allProjects.Add(new Project("8th", 500, 2, 5000));

        MarketProjects.instance.RenderProjects();

        InvokeRepeating("deductSalaries", EMPLOYEE_SALARY_FREQUENCY, EMPLOYEE_SALARY_FREQUENCY);
        InvokeRepeating("renderProjects", RENDER_FREQUENCY, RENDER_FREQUENCY);

        candidates = new List < Candidate > ();
        candidates.Add(new Candidate("Howard Wolowitz", 15, 5, 200, 1));
        candidates.Add(new Candidate("Eve Jobs", 1, 25, 200, 1));
        candidates.Add(new Candidate("Charles Babbage", 15, 5, 170, 1));
        candidates.Add(new Candidate("Barney Babbage", 15, 5, 150, 2));
        candidates.Add(new Candidate("Barney Sabotage", 35, -5, 180, 2));

        CandidateManager.instance.RenderCandidates();

    }

    // Update is called once per frame
    public void Update() {
        cashText.text = ((int) cash).ToString();
        projectsFinishedText.text = projectsFinished.ToString();
        revenueText.text = revenue.ToString();
        annotatorsCountText.text = annotatorsCount.ToString();
        occupiedAnnotatorsText.text = occupiedAnnotators.ToString();
        GameLevelText.text = (GameLevel + 1).ToString();

        daysSinceStart = DayCounter.Instance.getDays();
        daysSinceStartText.text = daysSinceStart.ToString();
        checkRevenueMilestones();
        runProjectsRoutine();
    }

    public List < TeamMember > getTeamMembers() {
        return teamMembers;
    }

    public void HireAnnotator()
    {
        annotatorsCount += 1;
    }

    public void FireAnnotator()
    {
        if ((annotatorsCount > 0) && (occupiedAnnotators < annotatorsCount))
        {
            annotatorsCount -= 1;
        }
        
    }

    public void HireCandidate(Candidate candidate) {
        teamMembers.Add(new TeamMember(candidate.Name, candidate.DevSkills, candidate.DesignSkills, candidate.Salary));
        TeamManager.instance.RenderTeamMembers();
        candidates.Remove(candidate);
        CandidateManager.instance.RenderCandidates();
        // Debug.Log("Candidate Hired: " + candidate.Name);
    }

    public void increaseRevenue(double increment) 
    {
        revenue += increment;
        cash += increment;
    }

    private void deductSalaries() {
        double totalMonthlySalary = 0;
        totalMonthlySalary += annotatorsCount * AnnotatorSalary;
        foreach(TeamMember teamMember in teamMembers) {
            totalMonthlySalary += teamMember.Salary;
        }
        cash -= totalMonthlySalary / 30;

        if (cash < 0) {
            SceneManager.LoadScene("GameOver_Screen");
        }
    }

    private void checkRevenueMilestones() {
        if (revenue >= revenueMilestones[(int) GameLevel].Milestone) {
            levelUp();
        }
    }

    public void levelUp() {
        GameLevel += LEVEL_INCREMENT;
        isCurrentLevelFeatureBuilt = false;
        MarketProjects.instance.RenderProjects();
        CandidateManager.instance.RenderCandidates();
        updateProductCardNameAndButtonState();
    }

    private void updateProductCardNameAndButtonState() {
        productCard.transform.Find("name").GetComponent < TMP_Text > ().text = PRODUCT_LEVEL_MAPPING[GameLevel + 1];
        productCard.transform.Find("Button").GetComponent < Button > ().interactable = true;
    }

    public string getCurrentLevelProductName() {
        return PRODUCT_LEVEL_MAPPING[GameLevel + 1];
    }

    public double getCurrentLevelProductDevSkillsTarget() {
        return PRODUCT_DEV_MAPPING[GameLevel + 1];
    }

    public double getCurrentTotalDevSkills() {
        double totalDevSkills = 0;
        foreach(TeamMember teamMember in teamMembers) {
            totalDevSkills += teamMember.DevSkills;
            // totalDesignSkills += teamMember.DesignSkills;
        }
        return totalDevSkills;
    }

    public double getCurrentLevelProductDesignSkillsTarget() {
        return PRODUCT_DESIGN_MAPPING[GameLevel + 1];
    }

    private void runProjectsRoutine() {
        foreach(Project project in allProjects) {
            if (project.ProjectState == ProjectState.STARTED) {
                double unitsSpent = (daysSinceStart - project.StartDay) * project.AssignedAnnotatorsToProject;
                if (unitsSpent >= project.UnitsToComplete) {
                    // project is complete
                    finishProject(project);
                }
            }
        }
    }

    private void renderProjects() {
        MarketProjects.instance.RenderProjects();
    }

    private void disableProjectButton(int projectIndex) {
        // Debug.Log(projectIndex);
        // Debug.Log(projectsPanel);
        // Debug.Log(projectsPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(projectIndex).transform.GetChild(6).GetComponent<Button>());
        // Debug.Log(projectsPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(projectIndex).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
        projectsPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(projectIndex).transform.GetChild(6).GetComponent < Button > ().interactable = false;
    }
}