using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraAdjuster : MonoBehaviour
{
    // https://www.youtube.com/watch?v=3xXlnSetHPM&ab_channel=PressStart

    public SpriteRenderer playArea;
    // Start is called before the first frame update
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = playArea.bounds.size.x / playArea.bounds.size.y;

        if(screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = playArea.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = playArea.bounds.size.y / 2 * differenceInSize;
        }
    }
}
