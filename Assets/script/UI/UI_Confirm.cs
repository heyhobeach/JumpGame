using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Confirm : MonoBehaviour
{
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private Button xButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cencelButton;

    void Start()
    {
        xButton.onClick.RemoveAllListeners();
        cencelButton.onClick.RemoveAllListeners();

        xButton.onClick.AddListener(Cancel);
        cencelButton.onClick.AddListener(Cancel);
    }

    private void Cancel()
    {
        confirmButton.onClick.RemoveAllListeners();
        this.gameObject.SetActive(false);
    }

    public void ConfirmSet(Action action, string info)
    {
        infoText.text = info;
        confirmButton.onClick.AddListener(()=>{
            this.gameObject.SetActive(false);
            action();
        });
    }
}
