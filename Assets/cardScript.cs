using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Feedbacks;

public class cardScript : MonoBehaviour
{

    [Header("Card Details")]
    [SerializeField] public string cardType;
    [SerializeField] public string cardCount;
    [SerializeField] public int indexInList;
    [SerializeField] public bool facedown = true;

    
    [SerializeField] private Sprite redSprite;
    [SerializeField] private Sprite yellowSprite;
    [SerializeField] private Sprite blueSprite;
    [SerializeField] private Sprite greenSprite;
    [SerializeField] private Sprite whiteSprite;
    [SerializeField] private Sprite blackSprite;

    [Header("card back")]
    [SerializeField] private GameObject CardBack;
    private SpriteRenderer backSR;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private Sprite greenBackSprite;

    [Header("card face")]
    [SerializeField] private GameObject CardFace;
    private SpriteRenderer faceSR;
    [SerializeField] public Sprite faceSprite;

    [SerializeField] public int ruleBackTextCounter;

    [SerializeField] private cardTextAssignerScript assignerScript;
    [SerializeField] public MMFeedbacks cardFeedback;
    // Start is called before the first frame update
    void Start()
    {
        transform.name = cardType + " " + cardCount;

        faceSR = CardFace.GetComponent<SpriteRenderer>();
        backSR = CardBack.GetComponent<SpriteRenderer>();

        

        spriteAssigner();
        cardFeedback?.Initialization();
        
    }
    private void Awake()
    {
        //backWriter();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //not used
    /*
    public void backWriter(int ruleCounter)
    {
        if (cardType == "green")
        {
            CardBackText.GetComponent<TextMeshPro>().text = ruleCounter.ToString();
            CardBackText.GetComponent<TextMeshPro>().enabled = false;
            if (ruleCounter != 9)
            {
                CardBackText.GetComponent<TextMeshPro>().enabled = false;
            }
        }
    }
    */

    public void spriteAssigner()
    {
        //this is if you have a giant list of pngs
        Debug.Log("index in list for this card(" + gameObject + "): " + indexInList);
        faceSR.sprite = assignerScript.calculateFace(cardType, indexInList);
        if (cardType == "green")
        {
            backSR.sprite = greenBackSprite;
        }
        if (cardType != "green")
        {
            backSR.sprite = backSprite;
        }
    }

    public void faceUp()    
    {
        facedown = false;
        CardBack.SetActive(facedown);
    }
    public void faceDown()
    {
        facedown = true;
        CardBack.SetActive(facedown);
    }

    public void flipCard()
    {
        if (facedown)
        {
            facedown = false;
            CardBack.SetActive(facedown);
        }
        else
        {
            facedown = true;
            CardBack.SetActive(facedown);
        }
    }
}
