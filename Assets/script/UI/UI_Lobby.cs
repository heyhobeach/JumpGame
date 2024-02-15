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
        
        optionButton.onClick.AddListener(SystemManager.Instance.ShowOption);
        startButton.onClick.AddListener(() => {
            SystemManager.LoadScene("SampleScene");
        });
    }
}
