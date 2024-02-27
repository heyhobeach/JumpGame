using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToLobby : EscapeOption
{
    public override void EscapeAction() => GameManager.Lobby();
}
