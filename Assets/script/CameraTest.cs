using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    private static CameraTest instance = null;

    public static CameraTest Instance
    {
        get
        {
            if (instance == null) return null;
            return instance;
        }
    }

    [SerializeField] private Camera backGroundCamera;
    [SerializeField] private Camera playerCamera;

    public float cameraSetTime;

    private Dictionary<CameraPreSet, CameraSetParameter> keyValuePairs = new();
    public enum CameraPreSet { SET1, SET2, SET3 }
    private struct CameraSetParameter
    {
        public Vector3 bgCameraPosition;
        private Vector3 plCameraPosition;
        public Vector3 PlCameraPosition { get { return bgCameraPosition + plCameraPosition; } }
        public float bgCameraSize;
        public float plCameraSize;

        public CameraSetParameter(Vector3 bgCameraPosition, Vector3 plCameraPosition, int bgCameraSize, float plCameraSize)
        {
            this.bgCameraPosition = bgCameraPosition;
            this.plCameraPosition = plCameraPosition;
            this.bgCameraSize = bgCameraSize;
            this.plCameraSize = plCameraSize;
        }
    }

    private static CameraPreSet m_cameraPreSet;
    public static CameraPreSet cameraPreSet
    {
        get { return m_cameraPreSet; }
        set
        {
            m_cameraPreSet = value;
            if(CameraTest.instance.coroutine != null) CameraTest.instance.StopCoroutine(CameraTest.instance.coroutine);
            CameraTest.instance.coroutine = CameraTest.instance.StartCoroutine(CameraTest.instance.CameraSetting(CameraTest.instance.cameraSetTime));
        }
    }

    void Awake() 
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance= this;
        DontDestroyOnLoad(this.gameObject);
        keyValuePairs.Add(CameraPreSet.SET1, new CameraSetParameter(new Vector3(0, -2.5f, -10), new Vector3(0, -1, -10), 5, 2.25f));
        keyValuePairs.Add(CameraPreSet.SET2, new CameraSetParameter(new Vector3(0, 0, -10), new Vector3(0, 0, -10), 5, 5));
        keyValuePairs.Add(CameraPreSet.SET3, new CameraSetParameter(new Vector3(0, -2.5f, -10), new Vector3(0, 0, -10), 5, 5));
    }

    void Start()
    {
        GameManager.LoadScene("Lobby", new AppQuit());
    }

    private Coroutine coroutine;

    private IEnumerator CameraSetting(float time)
    {
        float t = 0;
        Vector3 bgcp = backGroundCamera.transform.position;
        Vector3 plcp = playerCamera.transform.position;
        float bgcs = backGroundCamera.orthographicSize;
        float plcs = playerCamera.orthographicSize;
        while(t <= time)
        {
            t += Time.fixedUnscaledDeltaTime;
            backGroundCamera.transform.position = Vector3.Lerp(bgcp, keyValuePairs[cameraPreSet].bgCameraPosition, t / time);
            playerCamera.transform.position = Vector3.Lerp(plcp, keyValuePairs[cameraPreSet].PlCameraPosition, t / time);
            backGroundCamera.orthographicSize = Mathf.Lerp(bgcs, keyValuePairs[cameraPreSet].bgCameraSize, t / time);
            playerCamera.orthographicSize = Mathf.Lerp(plcs, keyValuePairs[cameraPreSet].plCameraSize, t / time);
            yield return null;
        }
        coroutine = null;
        yield break;
    }
}
