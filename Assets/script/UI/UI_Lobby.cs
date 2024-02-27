using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : MonoBehaviour
{
    [SerializeField] private Button skinButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button startButton;

    void Awake()
    {
        skinButton.onClick.AddListener(GameManager.Skin);
        optionButton.onClick.AddListener(new OptionMenu());
        startButton.onClick.AddListener(GameManager.Retry);
    }
}
