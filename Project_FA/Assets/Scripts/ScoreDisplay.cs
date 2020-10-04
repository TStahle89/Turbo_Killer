using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public static int scoreValue = 0;
    public Text score;

    void Update()
    {
        score.text = "Score\n" + scoreValue;
    }
}
