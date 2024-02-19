using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EscapeOption
{
    public abstract void EscapeAction();
    public static implicit operator System.Action(EscapeOption escapeOption)
    {
        return escapeOption.EscapeAction;
    }
    public static implicit operator UnityEngine.Events.UnityAction(EscapeOption escapeOption)
    {
        return escapeOption.EscapeAction;
    }
}
