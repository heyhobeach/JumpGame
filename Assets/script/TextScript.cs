using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public void showScore()
    {//Result<line-height=50>\n</line-height><sprite=0> X 0\n2.4

        scoreText.text = "GMAE OVER<line-height=50>\n</line-height><sprite=0> X 0\n" + GameManager.Instance.MakeScore()[0].ToString();
        highScoreText.text = "hight score\n<sprite=0> X 0\t" + GameManager.Instance.MakeScore()[1].ToString();
        //highScoreText.text
    }
    
}
