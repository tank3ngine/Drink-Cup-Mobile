using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Feedbacks;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{
    public MMFeedbacks newRuleFeedback;
    public MMFeedbacks fadeTextFeedback;
    private bool textFaded = false;
    [Header("Card variables")]
    [SerializeField] private int cardsToSpawn;
    [SerializeField] public int redNum;
    [SerializeField] public int yelNum;
    [SerializeField] public int bluNum;
    [SerializeField] public int greNum;
    [SerializeField] public int whiNum;
    [SerializeField] public int blaNum;
    private int redToSpawn;
    private int yellowToSpawn;
    private int blueToSpawn;
    private int greenToSpawn;
    private int whiteToSpawn;
    private int blackToSpawn;

    [Header("Game variables")]
    [SerializeField] private bool gameover = false;
    [SerializeField] private int sortingCounter = 0;
    [SerializeField] public int currentRuleIndex = 8;

    [Header("References")]
    [SerializeField] private GameObject drawPile;
    [SerializeField] private GameObject discardPile;
    [SerializeField] private GameObject rulePile;
    [SerializeField] private GameObject ruleDiscardPile;
    [SerializeField] private TMP_Text CardsLeftText;
    [SerializeField] private TMP_Text RuleCardsLeft;

    [Header("List Setup - All Cards")]
    [SerializeField] private GameObject _CardPrefab;
    [SerializeField] private List<GameObject> TotalCards = new List<GameObject>();
    [SerializeField] private List<GameObject> GreenCards = new List<GameObject>();
    [SerializeField] private List<GameObject> RedCards = new List<GameObject>();
    [SerializeField] private List<GameObject> BlueCards = new List<GameObject>();
    [SerializeField] private List<GameObject> YellowCards = new List<GameObject>();
    [SerializeField] private List<GameObject> BlackCards = new List<GameObject>();
    [SerializeField] private List<GameObject> WhiteCards = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //the one is because we want the count to start at 1
        cardsToSpawn = redNum + yelNum + bluNum + greNum + whiNum + blaNum;
        SpawnCards();
        CardsToLists();
        sortRuleCards();
        updateRemainingText();
        RuleCardsLeft.text = currentRuleIndex.ToString();

        //Initialize feedbacks (from MoreMountains)
        newRuleFeedback.Initialization();
        fadeTextFeedback.Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        //ruleDiscardPile.GetComponent<SpriteRenderer>().sprite = ruleDiscardPile.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }

    public void SpawnAssigner()
    {
     redToSpawn = redNum;
     yellowToSpawn = yelNum;
     blueToSpawn = bluNum;
     greenToSpawn = greNum;
     whiteToSpawn = whiNum;
     blackToSpawn = blaNum;
    }

    public void SpawnCards()
    {
        SpawnAssigner();
        for (int i = 0; i < cardsToSpawn; i++)
        {
            var tempCard = Instantiate(_CardPrefab, drawPile.transform.position, Quaternion.identity, transform);
            var tempCardScript = tempCard.GetComponent<cardScript>();

            TotalCards.Add(tempCard);

            tempCardScript.cardCount = i.ToString();
            //red
            if (redToSpawn >= 0)
            {
                tempCardScript.cardType = "red";
                redToSpawn--;
            }
            //yellow
            if (yellowToSpawn >= 0 && redToSpawn < 0)
            {
                tempCardScript.cardType = "yellow";
                yellowToSpawn--;
            }
            //blue
            if (blueToSpawn >= 0 && yellowToSpawn < 0)
            {
                tempCardScript.cardType = "blue";
                blueToSpawn--;
            }
            //green
            if (greenToSpawn >= 0 && blueToSpawn < 0)
            {
                tempCardScript.cardType = "green";
                //tempCard.transform.position = rulePile.transform.position;
                greenToSpawn--;
            }
            //white
            if (whiteToSpawn >= 0 && greenToSpawn < 0)
            {
                tempCardScript.cardType = "white";
                whiteToSpawn--;
            }

            //black
            if (blackToSpawn >= 0 && whiteToSpawn < 0)
            {
                tempCardScript.cardType = "black";
                blackToSpawn--;
            }
        }
    }

    public void CardsToLists()
    {
        //for each card in the deck, including rule cards
        foreach (var card in TotalCards)
        {
            cardScript tempCardScript = card.GetComponent<cardScript>();

            //check the card type and add each type to their appropriate list
            if (tempCardScript.cardType == "green")
            {
                GreenCards.Add(card);
                tempCardScript.indexInList = GreenCards.IndexOf(card);
            }
            if (tempCardScript.cardType == "red")
            {
                RedCards.Add(card);
                tempCardScript.indexInList = RedCards.IndexOf(card);
            }
            if (tempCardScript.cardType == "blue")
            {
                BlueCards.Add(card);
                tempCardScript.indexInList = BlueCards.IndexOf(card);
            }
            if (tempCardScript.cardType == "yellow")
            {
                YellowCards.Add(card);
                tempCardScript.indexInList = YellowCards.IndexOf(card);
            }
            if (tempCardScript.cardType == "white")
            {
                WhiteCards.Add(card);
                tempCardScript.indexInList = WhiteCards.IndexOf(card);
            }
            if (tempCardScript.cardType == "black")
            {
                BlackCards.Add(card);
                tempCardScript.indexInList = BlackCards.IndexOf(card);
            }
            Debug.Log(tempCardScript.indexInList);
            card.transform.parent = GameObject.Find("Draw Pile").transform;
        }
    }

    //For separating rule cards from green cards. Also sets the position of all the rule cards
    public void sortRuleCards()
    {
        foreach (var greenCard in GreenCards)
        {

            TotalCards.Remove(greenCard);
            int index = GreenCards.IndexOf(greenCard);

            if (index != 8)
            {
                greenCard.transform.position = rulePile.transform.position;
                greenCard.GetComponent<SpriteRenderer>().sortingOrder = index;
                greenCard.transform.parent = GameObject.Find("Table Rule Holder").transform;
            }

            else
            {
                //ruleDiscardPile.GetComponent<SpriteRenderer>().sprite = greenCard.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                greenCard.transform.position = ruleDiscardPile.transform.position;
                greenCard.transform.parent = GameObject.Find("Table Rule Discard").transform;
                greenCard.GetComponent<cardScript>().faceUp();
            }
        }
    }
    //Function for when any card is drawn (i.e. the screen is pressed). Does something else if the game is over.
    public void drawCard()
    {
        
        if (TotalCards.Count <= 0) 
        {
            gameover = true;
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(0);
            return;
        }

        if (!textFaded)
        {
            eraseText();
        }

        //int ranges are max exclusive
        int randomInList = Random.Range(0, TotalCards.Count);

        GameObject drawnCard = TotalCards[randomInList];
        SpriteRenderer tempSR = drawnCard.transform.GetChild(0).GetComponent<SpriteRenderer>();
        var tempScript = drawnCard.GetComponent<cardScript>();

        tempScript.cardFeedback?.PlayFeedbacks();
        //discardPile.GetComponent<SpriteRenderer>().sprite = tempSR.sprite;

        //disable card spriterenderers
        for (int i = 0; i < discardPile.transform.childCount; i++)
        {
            //disable the spriteRenderer: discardPile > card i > child 0
            discardPile.transform.GetChild(i).transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }

        drawnCard.transform.position = discardPile.transform.position;
        drawnCard.transform.parent = discardPile.transform;

        tempScript.faceUp();
        

        if (tempScript.cardType == "white")
        {
            //Debug.Log("white Found");
            whiteFound();
        }

        TotalCards.RemoveAt(randomInList);
        Debug.Log("TotalCards" + TotalCards.Count);

        tempSR.sortingOrder = sortingCounter;
        sortingCounter += 1;
        //Debug.Log(sortingCounter);

        updateRemainingText();
        //drawnCard.SetActive(false);
    }
    //Setting up the game for replay
    public void SetupGame()
    {
        currentRuleIndex = 8;
        SpawnCards();
        CardsToLists();
        sortRuleCards();
        updateRemainingText();
        RuleCardsLeft.text = currentRuleIndex.ToString();
    }

    public void whiteFound()
    {
        //draws a new rule/green card
        GameObject topCard = GreenCards[currentRuleIndex-1];
        cardScript topCardScript = topCard.GetComponent<cardScript>();

        topCardScript.cardFeedback?.PlayFeedbacks();
        newRuleFeedback?.PlayFeedbacks();
        textFaded = false;
        //disable spriterenderer
        for (int i = 0; i < ruleDiscardPile.transform.childCount; i++)
        {
            //disable the spriteRenderer: discardPile > card i > child 0
            ruleDiscardPile.transform.GetChild(i).transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
        //comes after the disabler
        topCard.transform.position = ruleDiscardPile.transform.position;
        topCard.transform.parent = ruleDiscardPile.transform;

        //topCard.transform.SetAsFirstSibling();
        topCardScript.faceUp();

        currentRuleIndex--;
        RuleCardsLeft.text = (currentRuleIndex).ToString();
        //topCard.SetActive(false);
    }

    public void updateRemainingText()
    {
        CardsLeftText.text = TotalCards.Count.ToString();
    }

    public void deleteCards()
    {
        if (GameObject.FindGameObjectsWithTag("Card") == null) { return; }

        foreach (var card in GameObject.FindGameObjectsWithTag("Card"))
        {
            Destroy(card);
        }
    }

    public void eraseText()
    {
        fadeTextFeedback.PlayFeedbacks();
        textFaded = true;
    }
}
