using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

struct PlayerData//json으로 변환할때 클래스 형식으로 변환 하려고
{
    public float highScore;
    public Sprite[] mapSprites;
    public Animation playerAni;
}

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    // void Awake()
    // {
    //     // PlayerPrefs.DeleteKey("HighScore");//최고기록 지우는용

    //     if (instance == null)
    //     {
    //         instance = this;

    //         DontDestroyOnLoad(this.gameObject);//gameobject
    //         DontDestroyOnLoad(GameObject.Find("PopupCanvas"));//retry를 누르고 나서도 계속 PopupCanvas를 연결시키기 위함
    //     }
    //     else
    //     {
    //         Destroy(this.gameObject);
    //     }
    //     SetPopup();



    // }

    // public void SetPopup()//초기 팝업창들 전부 false로 가려주기위함
    // {
    //     GameoverPopup.gameObject.SetActive(false);//초기 게임 ui 초기화 나중에 함수로 묶을예정
    // }

    // public void PopupHandler()//아직은 인자가 없지만 나중에 팝업창이 여러개라면 여기서 팝업 관리 할 예정
    // {
    //     //죽는팝업
    //     GameoverPopup.gameObject.SetActive(true);

    // }

    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (instance == null) return null;
            return instance;
        }
    }

    //public GameObject player;
    public Stack<PopUp> popUps = new();
    public Action escapeEvent { get; set; }

    public GameObject optionCanvas { get; private set; }
    public GameObject confirmCanvas { get; private set; }
    public GameObject gameOverCanvas { get; private set; }
    public GameObject CountDownCanvas { get; private set; }
    
    void Awake() 
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance= this;
        DontDestroyOnLoad(this.gameObject);

        escapeEvent = new AppQuit();

        optionCanvas = Instantiate(Resources.Load("Prefabs/UI/Option Canvas") as GameObject, this.transform);
        optionCanvas.SetActive(false);
        confirmCanvas = Instantiate(Resources.Load("Prefabs/UI/Confirmation Canvas") as GameObject, this.transform);
        confirmCanvas.SetActive(false);
        gameOverCanvas = Instantiate(Resources.Load("Prefabs/UI/Game Over Canvas") as GameObject, this.transform);
        gameOverCanvas.SetActive(false);
        CountDownCanvas = Instantiate(Resources.Load("Prefabs/UI/CountDown Canvas") as GameObject, this.transform);
        CountDownCanvas.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(popUps.Count > 0) popUps.Pop().gameObject.SetActive(false);
            else { if(escapeEvent != null) escapeEvent(); } 
        }
    }


    public List<float> MakeScore()//점수 만들어서 리턴 할 예정
    {
        if(CameraSet.cameraInstance == null) return null;
        //Score _score = new Score();
        List<float> score = new List<float>() { 0, 0 };//score[0]에는 현재 점수, score[1]에는 최고 점수
        float high = CameraSet.cameraInstance.high;
        score[0] = high;
        if (high > PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", high);//여기 부터 최고 점수 저장 부분if문안에 있는건 전부 함수로 묶어도 괜찮음

            //_score.highScore = high;
            //string jsonData = JsonUtility.ToJson(_score);
            //Debug.Log(jsonData);

        }
        score[1] = PlayerPrefs.GetFloat("HighScore");

        return score;
    }

    public void Dead()//죽을때 불러올 함수 
    {
        //popup
        gameOverCanvas.SetActive(true);
        Pause();
        //대충 사망 관리 처리
    }


    public static void Pause() => Time.timeScale = 0; //일시정지 함수
    public static void Play() => Time.timeScale = 1; // 재시작 함수

    public static void LoadScene(string sceneName, EscapeOption escapeOption = null, CameraTest.CameraPreSet cameraPreSet = CameraTest.CameraPreSet.SET1) //씬 로드 함수
    {
        if(Time.timeScale != 1) GameManager.Play();
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        ao.completed += (x)=>{
            if(escapeOption != null) GameManager.instance.escapeEvent = escapeOption; 
            CameraTest.cameraPreSet = cameraPreSet;
        };
    }
    public static void Retry() => LoadScene("SampleScene", new PauseMenu(), CameraTest.CameraPreSet.SET2); //재시작 함수
    public static void Lobby() => LoadScene("Lobby", new AppQuit(), CameraTest.CameraPreSet.SET1);//로비로 돌아가는함수
    public static void Skin() => LoadScene("Skin", new BackToLobby(), CameraTest.CameraPreSet.SET1);//로비로 돌아가는함수

}
