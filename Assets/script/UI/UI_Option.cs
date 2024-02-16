using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Option : MonoBehaviour
{
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private Button xButton;
    [SerializeField] private Transform buttonsParent;
    [SerializeField] private Button[] optionButtons;
    private List<Button> appendButton = new();
    
    void Start()
    {
        xButton.onClick.RemoveAllListeners();

        xButton.onClick.AddListener(()=>{ this.gameObject.SetActive(false); });
        this.gameObject.SetActive(false);
    }

    public void SetInfoText(string text) => infoText.text = text;
    public void AddButton(string name, Action action)
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
