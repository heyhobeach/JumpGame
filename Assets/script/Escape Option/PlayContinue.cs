using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayContinue : EscapeOption
{
    public override void EscapeAction()
    {
        GameManager.Instance.optionCanvas.SetActive(false);
        GameManager.Instance.CountDownCanvas.SetActive(true);
    }
}
