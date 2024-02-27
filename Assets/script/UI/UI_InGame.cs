using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private Button pauseButton;

    void Awake() => pauseButton.onClick.AddListener(new PauseMenu());
}
