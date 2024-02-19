using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : EscapeOption
{
    public override void EscapeAction()
    {
        GameManager.Pause();
        UI_Option temp = GameManager.Instance.optionCanvas.GetComponent<UI_Option>();
        temp.RemoveAllButtons();
        temp.SetInfoText("Pause");
        temp.SetEscButton(new PlayContinue());
        temp.AddButton("Quit", () => {
            GameManager.Lobby();
            GameManager.Instance.optionCanvas.SetActive(false);
        });
        temp.AddButton("Play", new PlayContinue());
        GameManager.Instance.optionCanvas.SetActive(true);
    }
}
