using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button retry, lobby;

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
        retry.onClick.AddListener(test);
        lobby.onClick.AddListener(GameManager.Instance.Lobby);
    }

    public void test()
    {
        GameManager.Instance.Retry();
        GameManager.Instance.SetPopup();
        //Debug.Log("이벤트 테스트");
    }
}
