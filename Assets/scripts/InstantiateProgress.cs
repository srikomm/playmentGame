using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateProgress : MonoBehaviour
{
    [SerializeField] public Transform canvas;
    [SerializeField] public Button buildProductBtn;
    private GameObject cardPrefab;
    private GameObject newCard;

    // Start is called before the first frame update
    void Start()
    {
        cardPrefab = Resources.Load("ProgressCard") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (newCard && newCard.GetComponent<ProgressBar2>().HasProgressCompleted())
        {
            
            Destroy(newCard);
        }
    }

    public void InstantiateProgressCard()
    {
        newCard = Instantiate(cardPrefab, canvas);
        buildProductBtn.interactable = false;
    }
}
