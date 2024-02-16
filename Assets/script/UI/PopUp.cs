using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopUp : MonoBehaviour
{
    public abstract void EscPopUp();
    void OnEnable() => SystemManager.Instance.popUps.Push(this);
    void OnDisable()
    {
        if(!SystemManager.Instance.popUps.Contains(this)) return;
        if(SystemManager.Instance.popUps.Peek().Equals(this)) 
            SystemManager.Instance.popUps.Pop();
    }
}
