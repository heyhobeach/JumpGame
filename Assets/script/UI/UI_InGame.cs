using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private Button pauseButton;

    void Start()
    {
        pauseButton.onClick.RemoveAllListeners();

        pauseButton.onClick.AddListener(() => {
            Time.timeScale = 0;
            UI_Option temp = SystemManager.Instance.optionCanvas.GetComponent<UI_Option>();
            temp.RemoveAllButtons();
            temp.SetInfoText("Pause");
            temp.AddButton("Quit", () => {
                SystemManager.LoadScene("Lobby");
                SystemManager.Instance.ActiveOption(false);
            });
            temp.AddButton("Play", () => {
                SystemManager.Instance.ActiveOption(false);
                Time.timeScale = 1;
            });
            SystemManager.Instance.ActiveOption(true);
        });
    }
}
