using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_GameOver : PopUp
{
    [SerializeField] private Button retry, lobby;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    void Awake()
    {
        retry.onClick.AddListener(()=>{
            GameManager.Retry();
            gameObject.SetActive(false);
        });
        lobby.onClick.AddListener(()=>{
            GameManager.Lobby();
            gameObject.SetActive(false);
        });
    }

    public override void EscPopUp()
    {
        GameManager.Play();
    }

    protected new void OnEnable()
    {
        base.OnEnable();
        showScore();
    }

    public void showScore()
    {//Result<line-height=50>\n</line-height><sprite=0> X 0\n2.4
        List<float> temp = GameManager.Instance.MakeScore();
        if(temp == null) return;
        scoreText.text = $"GMAE OVER<line-height=50>\n</line-height><sprite=0> X 0\n{temp[0]}";
        highScoreText.text = $"hight score\n<sprite=0> X 0\t{temp[1]}";
        //highScoreText.text
    }
}
