using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : EscapeOption
{
    public override void EscapeAction()
    {
        UI_Option temp = GameManager.Instance.optionCanvas.GetComponent<UI_Option>();
        temp.RemoveAllButtons();
        temp.SetInfoText("Option");
        temp.SetEscButton(()=>{ });
        temp.AddButton("Rank");
        temp.AddButton("Credit");
        GameManager.Instance.optionCanvas.SetActive(true);
    }
}
