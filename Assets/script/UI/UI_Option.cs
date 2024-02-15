using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Option : MonoBehaviour
{
    [SerializeField] private Button xButton;
    [SerializeField] private Button[] optionButtons;
    
    void Start()
    {
        xButton.onClick.RemoveAllListeners();

        xButton.onClick.AddListener(()=>{
            this.gameObject.SetActive(false);
        });
        this.gameObject.SetActive(false);
    }
}
