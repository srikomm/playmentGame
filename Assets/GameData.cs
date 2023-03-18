using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public double revenue;
    public double days;
     public static GameData Instance { get; private set; }

    void Awake()
    {
        
      if (Instance != null && Instance != this) 
        { 
           Destroy(this); 
        } 
      else 
        { 
           Instance = this; 
        } 
        
        DontDestroyOnLoad(this.gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        revenue = Controller.instance.revenue;
        Debug.Log("D: " + revenue);
        days = Controller.instance.daysSinceStart;
    }
}
