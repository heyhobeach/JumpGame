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

    void Start()
    {
        optionButton.onClick.RemoveAllListeners();
        startButton.onClick.RemoveAllListeners();
        
        optionButton.onClick.AddListener(() => {
            Time.timeScale = 0;
            UI_Option temp = SystemManager.Instance.optionCanvas.GetComponent<UI_Option>();
            temp.RemoveAllButtons();
            temp.SetInfoText("Option");
            temp.AddButton("Rank", null);
            temp.AddButton("Credit", null);
            SystemManager.Instance.ActiveOption(true);
        });
        startButton.onClick.AddListener(() => { SystemManager.LoadScene("SampleScene"); });
    }
}
