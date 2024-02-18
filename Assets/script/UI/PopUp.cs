using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopUp : MonoBehaviour
{
    public abstract void EscPopUp();
    protected void OnEnable() => GameManager.Instance.popUps.Push(this);
    protected void OnDisable()
    {
        if(!GameManager.Instance.popUps.Contains(this)) return;
        if(GameManager.Instance.popUps.Peek().Equals(this)) 
            GameManager.Instance.popUps.Pop();
    }
}
