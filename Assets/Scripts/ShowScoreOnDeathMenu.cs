using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScoreOnDeathMenu : MonoBehaviour
{
	public TextMeshProUGUI score;
    void Start()
    {
        score.text = Pterodyactl.finalScorePterodyactl.ToString();
    }
}
