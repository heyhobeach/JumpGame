using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopUp : MonoBehaviour
{
    public abstract void EscPopUp();
    protected void OnEnable() => GameManager.Instance.popUps.Push(this);
    protected void OnDisable() => EscPopUp();
}
