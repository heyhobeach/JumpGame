using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    
    public void SetPopup()
    {
        GameoverPopup.gameObject.SetActive(false);//초기 게임 ui 초기화 나중에 함수로 묶을예정
    }

    public void Dead()
    {
        //popup
        PopupHandler();
        Pause();
        //대충 사망 관리 처리
    }

    public void PopupHandler()
    {
        //죽는팝업
        GameoverPopup.gameObject.SetActive(true) ;

    }

    public void Pause()
    {
        //일시정지
        Time.timeScale = 0;
    }

    public void Retry()
    {
        Debug.Log("Retry");
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;//pause 시킨것 다시 풀어줌
    }

    public void Lobby()
    {
        //로비 내용
    }

}
