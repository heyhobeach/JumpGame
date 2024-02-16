using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Confirm : PopUp
{
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private Button escButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cencelButton;

    void Awake()
    {
        escButton.onClick.AddListener(EscPopUp);
        cencelButton.onClick.AddListener(EscPopUp);
    }

    public override void EscPopUp()
    {
        confirmButton.onClick.RemoveAllListeners();
        this.gameObject.SetActive(false);
    }

    public void ConfirmSet(string info, Action action)
    {
        infoText.text = info;
        confirmButton.onClick.AddListener(()=>{
            this.gameObject.SetActive(false);
            action();
        });
    }
}
