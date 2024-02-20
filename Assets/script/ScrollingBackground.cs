using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] background;//배경 길이가 대충 46이상
    public Transform temp;
    private float scrollhigh;
    int i, j;


    private void Scrolling()
    {
        Debug.Log("최고 높이 "+ CameraSet.cameraInstance.high + "스크롤 높이"+scrollhigh);
        if(CameraSet.cameraInstance.high > scrollhigh)
        {
            j = i - 1;
            if (i >= background.Length)
            {
                
                i = 0;
            }
            temp = background[i];
            Debug.Log("i =" + i + "j =" + j);
            background[i].position = new Vector3(background[j].position.x, background[j].position.y + 49,10);
            //background[j] = temp;

            scrollhigh +=49;
            i++;
            Debug.Log("스크롤링");
        }
    }


    void Start()
    {
        i = 2;
        //j = 1;
        //background = GetComponentsInChildren<Transform>();
        for (int i = 0; i < background.Length; i++)
        {
            background[i].GetComponent<Transform>();
        }

        scrollhigh = 32;
        //Debug.Log(Camera.main.WorldToViewportPoint(background[0].position));
    }

    // Update is called once per frame
    void Update()
    {
        Scrolling();
    }
}
