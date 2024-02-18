using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : MonoBehaviour
{
    [SerializeField] private Button AchivementButton;
    [SerializeField] private Button skinButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button startButton;

    void Awake()
    {
        optionButton.onClick.AddListener(() => {
            UI_Option temp = GameManager.Instance.optionCanvas.GetComponent<UI_Option>();
            temp.RemoveAllButtons();
            temp.SetInfoText("Option");
            temp.AddButton("Rank");
            temp.AddButton("Credit");
            GameManager.Instance.optionCanvas.SetActive(true);
        });
        startButton.onClick.AddListener(() => { GameManager.LoadScene("SampleScene"); });
    }
}
