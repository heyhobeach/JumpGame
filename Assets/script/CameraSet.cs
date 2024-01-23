using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    public static float cameraZpos;
    public float ratio=0.33f;

    public static float limitPos;
    public static Vector2 Top;
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

    public void SetScale()//해상도 설정은 camera에서
    {
        //Debug.Log(Screen.height);
        //Debug.Log(Screen.width);
    }

    void Awake()
    {
        //Screen.SetResolution(1848, 2960, true);
        Screen.SetResolution(924, 1480 , true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
