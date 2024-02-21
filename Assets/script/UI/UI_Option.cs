using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEditor.Build.Player;

public class UI_Option : PopUp
{
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private TMP_Text versionText;
    [SerializeField] private Button escButton;
    [SerializeField] private Transform buttonsParent;
    [SerializeField] private Button[] optionButtons;
    private List<Button> appendButton = new();
    private Action escapeOption;
    
    void Awake()
    {
        escButton.onClick.AddListener(EscPopUp);
        versionText.text = "Current Version. " + Application.version;
    }

    public override void EscPopUp()
    {
        if(escapeOption != null) escapeOption();
        else GameManager.Play();
    }
    public void SetInfoText(string text) => infoText.text = text;
    public void SetEscButton(Action action) => this.escapeOption = action;
    public void AddButton(string name, Action action = null)
    {
        Button tempButton = Instantiate(Resources.Load("Prefabs/UI/Button") as GameObject, buttonsParent).GetComponent<Button>();
        tempButton.GetComponentInChildren<TMP_Text>().text = name;
        if(action != null) tempButton.onClick.AddListener(()=>{ action(); });
        appendButton.Add(tempButton);
    }
    public void RemoveAllButtons()
    {
        if(appendButton.Count <= 0) return;
        foreach(Button b in appendButton) Destroy(b.gameObject);
        appendButton.Clear();
    }
}
