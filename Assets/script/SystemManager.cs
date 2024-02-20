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

    public Stack<PopUp> popUps = new();

    public GameObject optionCanvas { get; private set; }
    public GameObject confirmCanvas { get; private set; }

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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(popUps.Count > 0)
            {
                popUps.Peek().EscPopUp();
                return;
            }
            else
            {
                UI_Confirm temp = confirmCanvas.GetComponent<UI_Confirm>();
                temp.ConfirmSet("게임을 종료하시겠습니까?", Application.Quit);
                confirmCanvas.SetActive(true);
            }
        } 
    }

    public static void LoadScene(string sceneName)
    {
        if(Time.timeScale != 1) Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
