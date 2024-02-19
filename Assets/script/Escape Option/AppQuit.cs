using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppQuit : EscapeOption
{
    public override void EscapeAction()
    {
        UI_Confirm temp = GameManager.Instance.confirmCanvas.GetComponent<UI_Confirm>();
        temp.ConfirmSet("게임을 종료하시겠습니까?", Application.Quit);
        GameManager.Instance.confirmCanvas.SetActive(true);
    }
}
