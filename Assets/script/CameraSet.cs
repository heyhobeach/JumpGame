using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    public  float cameraZpos;//���� ����ƽ���� �� �ʿ������ �׳� ī�޶� �ν��Ͻ� ���� ���� ����Ż ���ѹ��19:28�� ����
    public float ratio=0.33f;

    public  float limitPos;
    public  Vector2 Top;
    public  static CameraSet cameraInstance;
    public TestScript player;
    // Start is called before the first frame update
    void Start()
    {
        cameraZpos = transform.position.z;
        Vector2 Right = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height * 0.5f));
        Vector2 Left = -Right;
        Top = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height));
        Vector2 Bottom = -Top;
        limitPos = Bottom.y - cameraZpos * ratio;
        Debug.Log(string.Format("top :{0}, bottom:{1}, left:{2}, right{3}",Top.y,Bottom.y,Left.x,Right.x));
        Debug.Log(string.Format("1/3 = {0}", Bottom.y - cameraZpos * ratio));
        //Debug.Log(cameraZpos);
    }

    public void SetScale()//�ػ� ������ camera����
    {
        //Debug.Log(Screen.height);
        //Debug.Log(Screen.width);
    }

    void Awake()
    {
        //Screen.SetResolution(1848, 2960, true);
        cameraInstance = this;
        Screen.SetResolution(924, 1480 , true);
    }
    private void FixedUpdate()
    {
        //if()
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(string.Format("�÷��̾� ��ġ :{0} ī�޶� ��ġ :{1}", player.transform.position, transform.position));
        Debug.Log(string.Format("ī�޶� ����Ʈ {0}", Camera.main.WorldToViewportPoint(player.transform.position)));
    }
}
