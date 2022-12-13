using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cardTextAssignerScript : MonoBehaviour
{
    [SerializeField] private cardScript thisCardScript;
    [SerializeField] private GameObject CardHolder;
    [SerializeField] private GameEngine GameEngineScript;
    [SerializeField] public int indexInList;
    [SerializeField] public string cardType;

    //Lists of Card Types
    [SerializeField] private List<Sprite> BlueCardsSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> RedCardsSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> YellowCardsSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> GreenCardsSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> WhiteCardsSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> BlackCardsSprites = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        CardHolder = GameObject.Find("Card Holder");
        GameEngineScript = CardHolder.GetComponent<GameEngine>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Sprite calculateFace(string type, int i)
    {
        Sprite cardFace = null;

        if (type == "blue")
        {
            cardFace = BlueCardsSprites[i];
        }
        if (type == "red")
        {
            cardFace = RedCardsSprites[i];
        }
        if (type == "yellow")
        {
            cardFace = YellowCardsSprites[i];
        }
        if (type == "green")
        {
            cardFace = GreenCardsSprites[i];
        }
        if (type == "white")
        {
            cardFace = WhiteCardsSprites[i];
        }
        if (type == "black")
        {
            cardFace = BlackCardsSprites[i];
        }

        return cardFace;
    }
}
