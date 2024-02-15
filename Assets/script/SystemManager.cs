using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{
    private static SystemManager instance = null;

    public static SystemManager Instance
    {
        get
        {
            if(instance == null) return null;
            return instance;
        }
    }

    private GameObject optionCanvas;
    private GameObject confirmCanvas;

    void Awake() 
    {
        if (instance == null)
        {
            instance= this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
        {
            Destroy(this.gameObject);
            return;
        }

        optionCanvas = Instantiate(Resources.Load("Prefabs/UI/Option Canvas") as GameObject, this.transform);
        optionCanvas.SetActive(false);
        confirmCanvas = Instantiate(Resources.Load("Prefabs/UI/Confirmation Canvas") as GameObject, this.transform);
        confirmCanvas.SetActive(false);
    }

    public static void LoadScene(string sceneName)
    {
        if(Time.timeScale != 1) Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void ShowOption() => optionCanvas.SetActive(true);
    public void ShowConfirm() => confirmCanvas.SetActive(true);
}
