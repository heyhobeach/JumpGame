using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomPattern : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] pList;
    public GameObject temp;
    //public Transform[] pList;
    //public Transform temp;
    public GameObject[] objList;
    public GameObject tempObj;
    int i=3, j=0;
    private float pHigh = 12.5f;
    const float cycle = 12.5f;
    private void Update()
    {
        //int num =Random.Range(0, 16);
        //Debug.Log("랜덤 숫자 출력" + num);
        InfinityPattern();


    }
    private void Start()
    {
        for(int i=0;i<pList.Length; i++) 
        {
            pList[i].GetComponent<Transform>();        
        }
    }
    [ContextMenu("click")]
    void test()
    {
        //tPrefab=Resources.Load("Capsule") as GameObject; 
        //tPrefab.transform.position= tObject.transform.position; 
        int rNum = Random.Range(0,pList.Length);
        tempObj = objList[rNum].gameObject;
        temp = pList[rNum];
        int lNum = Random.Range(0, pList.Length);
        //pList[rNum].transform.position = pList[lNum].transform.position;
        //pList[lNum].transform.position = temp.transform.position;

        temp = pList[rNum];
        Vector3 tempvec = temp.transform.position;
        pList[rNum].transform.position = pList[lNum].transform.position;
        pList[lNum].transform.position=tempvec;
        //pList[3] = temp;
        Debug.Log(string.Format("rNum : {0} lNum : {1}", pList[0].transform.position,temp.transform.position));
        Debug.Log("click");

    }

    private void InfinityPattern()
    {
        if (CameraSet.cameraInstance.high > pHigh)
        {
            int rand = Random.Range(0, objList.Length);
            //temp= pList[rand];

            j = i - 1;
            if (i >= pList.Length)
            {
                i = 0;
            }

            Debug.Log(string.Format("i : {0}, j : {1}", i, j));
            //pList[i] = temp;
            temp = objList[rand];
            Vector3 tempvec = temp.transform.position;
            //pList[i].transform.position = new Vector3(pList[j].transform.position.x, pList[j].transform.position.y + cycle, 0);
            temp.transform.position = new Vector3(pList[j].transform.position.x, pList[j].transform.position.y + cycle, 0);
            pList[i].transform.position = tempvec;

            objList[rand] = pList[i];
            pList[i] = temp;
            
            //pList[i].position = new Vector3(pList[j].position.x, pList[j].position.y + cycle, 0);
            pHigh += cycle;
            Debug.Log("높이 갱신" + pHigh);
            i++;

        }
    }
}
