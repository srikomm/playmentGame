using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevenueManager : MonoBehaviour

{
    public void increaseRevenue(double increment)
    {
        Controller.instance.revenue += increment;

        if(Controller.instance.revenue >= Controller.instance.revenueMilestones[Controller.instance.GameLevel-1].Milestone)
        {
            Controller.instance.increaseLevel(1);
        }
    }
}
