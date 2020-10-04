using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Text healthText;

    void Update()
    {
        healthText.text = "Health\n" + PlayerController.hp + "/5";
    }
}
