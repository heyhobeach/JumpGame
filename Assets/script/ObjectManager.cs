using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // Start is called before the first frame update

    //private static ObjectManager instance;//싱글톤 필요없어 보이긴함
    //public static ObjectManager Instance
    //{
    //    get
    //    {
    //        if(instance == null)
    //        {
    //            return null;
    //        }
    //        else
    //        {
    //            return instance;
    //        }
    //    }
    //}
    //
    //private void Awake()
    //{
    //    if(instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(instance);
    //    }
    //}

    public enum WallType {NONE, DEAD_WALL,ICE_WALL,STICK_WALL};

    public WallType type;



}
