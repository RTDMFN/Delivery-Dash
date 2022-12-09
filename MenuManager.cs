using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    
    public TextMeshProUGUI timeRemainingText;
    public TextMeshProUGUI scoreText;

    private void Awake(){
        instance = this;
    }

    private void Update(){
        UpdateRemainingTimeText();
        UpdateScoreText();
    }

    private void UpdateRemainingTimeText(){
        timeRemainingText.text = GameManager.instance.timeRemainingInMatch.ToString("0");
    }

    private void UpdateScoreText(){
        scoreText.text = GameManager.instance.score.ToString();
    }
    
}
