using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Skin : MonoBehaviour
{
    [SerializeField] private Button homeButton;
    [SerializeField] private Toggle swichToggle;

    void Awake()
    {
        homeButton.onClick.AddListener(new BackToLobby());
        swichToggle.onValueChanged.AddListener((b) => {
            if(b) CameraTest.cameraPreSet = CameraTest.CameraPreSet.SET1;
            else CameraTest.cameraPreSet = CameraTest.CameraPreSet.SET3;
        });
    }
}
