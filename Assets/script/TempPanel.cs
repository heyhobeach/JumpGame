using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TempPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private static TempPanel instance;
    public static TempPanel Instance
    {
        get
        {
            if(instance == null) return null;
            return instance;
        }
    }

    public enum InputState { None, Touching, Touch, Drag, Hold }
    public InputState inputState { get; private set; } = InputState.None;
    public Vector2 dir { get; private set; }

    private int pointerId;

    public void Awake() => instance = this;
    public void Start() => StartCoroutine("ResetState", 0);

    private IEnumerator ResetState()
    {
        while(true)
        {
            yield return new WaitUntil(() => inputState != InputState.None && inputState != InputState.Touching);
            inputState = InputState.None;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(pointerId != eventData.pointerId) return;
        // Debug.Log("DragBegin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(pointerId != eventData.pointerId) return;
        // Debug.Log("Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(pointerId != eventData.pointerId) return;
        inputState = InputState.Drag;
        dir = eventData.position - eventData.pressPosition;
        dir = dir.normalized;
        // Debug.Log("DragEnd");
        // Debug.Log("Dir : " + dir);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(inputState != InputState.None || inputState != InputState.Touching) return;
        inputState = InputState.Touching;
        pointerId = eventData.pointerId;
        dir = Vector2.zero;
        // Debug.Log("Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(pointerId != eventData.pointerId) return;
        inputState = InputState.Touch;
        // Debug.Log("Up");
    }
}
