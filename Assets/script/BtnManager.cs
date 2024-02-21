using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button retry, lobby;//버튼 이벤트 부여하려고 존재

    private static BtnManager instance;
    public static BtnManager Instance
    {
        get 
        {
            if(instance == null) instance = null;
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        //retry.onClick.AddListener(GameManager.Instance.Retry);
        retry.onClick.AddListener(test);//리트라이
        lobby.onClick.AddListener(GameManager.Instance.Lobby);
    }

    public void test()
    {
        GameManager.Instance.Retry();//재시작
        GameManager.Instance.SetPopup();//팝업끄고
        //Debug.Log("이벤트 테스트");
    }
}
