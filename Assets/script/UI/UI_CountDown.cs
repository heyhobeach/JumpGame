using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CountDown : PopUp
{
    const int COUNT = 3;
    [SerializeField] private TMP_Text countDownText;

    protected new void OnEnable()
    {
        base.OnEnable();
        StartCoroutine("CountDown");
    }

    private IEnumerator CountDown()
    {
        GameManager.Pause();
        int i = 0;
        while(COUNT > i)
        {
            countDownText.text = (COUNT - i++).ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        GameManager.Play();
        gameObject.SetActive(false);
    }

    public override void EscPopUp()
    {
        StopCoroutine("CountDown");
    }
}
