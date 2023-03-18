using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstantiateProductProgress : MonoBehaviour
{
    [SerializeField] public Transform canvas;
    [SerializeField] public Button buildProductBtn;
    private GameObject cardPrefab;
    private GameObject newCard;
    // private TMP_Text progressCardName;

    // Start is called before the first frame update
    void Start() {
        cardPrefab = Resources.Load("ProgressCard") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (newCard && newCard.GetComponent<ProductProgressBar>().HasProgressCompleted())
        {
            
            Destroy(newCard);
        }
    }

    public void InstantiateProductProgressCard()
    {
        newCard = Instantiate(cardPrefab, canvas);
        newCard.transform.Find("name").GetComponent < TMP_Text > ().text = Controller.instance.getCurrentLevelProductName();
        buildProductBtn.interactable = false;
    }

    public void UpdateButtonState() {
        // newCard.transform.Find("name").GetComponent<TMP_Text>().text = currentLevelProductName;
        buildProductBtn.interactable = true;
    }
}