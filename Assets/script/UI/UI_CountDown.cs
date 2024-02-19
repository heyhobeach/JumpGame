using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CountDown : MonoBehaviour
{
    [SerializeField] private TMP_Text countDownText;
    const int COUNT = 3;

    void OnEnable() => StartCoroutine("CountDown");

    // public void StartCountDown()
    // {
    //     gameObject.SetActive(true);
    //     StartCoroutine("CountDown");
    // }
    private IEnumerator CountDown()
    {
        GameManager.Pause();
        int i = 0;
        while(COUNT > i)
        {
            countDownText.text = (COUNT - i++).ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        GameManager.Continue();
        gameObject.SetActive(false);
    }
}
