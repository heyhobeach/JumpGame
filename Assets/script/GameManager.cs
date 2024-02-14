using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class Score//json으로 변환할때 클래스 형식으로 변환 하려고
{
    public float highScore;

}

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static GameManager instance=null;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }return instance;
        }
    }

    //public GameObject player;

    public Image GameoverPopup;

    void Awake() 
    {
       // PlayerPrefs.DeleteKey("HighScore");//최고기록 지우는용

        if (instance == null)
        {
            instance= this;

            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(GameObject.Find("PopupCanvas"));//retry를 누르고 나서도 계속 PopupCanvas를 연결시키기 위함
        }
        else
        {
            Destroy(this.gameObject);
        }
        SetPopup();

        

    }


    public List<float> MakeScore()//점수 만들어서 리턴 할 예정
    {
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
        score[1] =PlayerPrefs.GetFloat("HighScore");

        return score;
    }
    
    public void SetPopup()//초기 팝업창들 전부 false로 가려주기위함
    {
        GameoverPopup.gameObject.SetActive(false);//초기 게임 ui 초기화 나중에 함수로 묶을예정
    }

    public void Dead()//죽을때 불러올 함수 
    {
        //popup
        PopupHandler();
        GetComponent<TextScript>().showScore();
        Pause();
        //대충 사망 관리 처리
    }

    public void PopupHandler()//아직은 인자가 없지만 나중에 팝업창이 여러개라면 여기서 팝업 관리 할 예정
    {
        //죽는팝업
        GameoverPopup.gameObject.SetActive(true) ;

    }

    public void Pause()//일시정지 함수
    {
        //일시정지
        Time.timeScale = 0;
    }

    public void Retry()//재시작 함수
    {
        Debug.Log("Retry");
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;//pause 시킨것 다시 풀어줌
    }

    public void Lobby()//로비로 돌아가는함수
    {
        //로비 내용
    }

}
