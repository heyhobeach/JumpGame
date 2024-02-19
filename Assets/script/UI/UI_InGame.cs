using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private Button pauseButton;

    void Awake()
    {
        pauseButton.onClick.AddListener(() => {
            GameManager.Pause();
            UI_Option temp = GameManager.Instance.optionCanvas.GetComponent<UI_Option>();
            temp.RemoveAllButtons();
            temp.SetInfoText("Pause");
            temp.AddButton("Quit", () => {
                GameManager.LoadScene("Lobby");
                GameManager.Instance.optionCanvas.SetActive(false);
            });
            temp.AddButton("Play", () => {
                GameManager.Instance.optionCanvas.SetActive(false);
                GameManager.Instance.CountDownCanvas.SetActive(true);
            });
            GameManager.Instance.optionCanvas.SetActive(true);
        });
    }

    
}
