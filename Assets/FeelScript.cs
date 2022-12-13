using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class FeelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public MMFeedbacks demoCard;

    public void playDemoCardFeedback()
    {
        demoCard.PlayFeedbacks();
    }
}
