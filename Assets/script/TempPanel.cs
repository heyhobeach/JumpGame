using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//전체적인 터치 조작을 관리하기위한 부분
public class TempPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler//터치 관련 인터페이스
{
    private static TempPanel instance;//싱글톤 사용을 위해
    public static TempPanel Instance
    {
        get
        {
            if(instance == null) return null;
            return instance;
        }
    }

    public enum InputState { None, Touching, Touch, Drag, Hold }//case 에 넣으려고 열거형
    public InputState inputState { get; private set; } = InputState.None;//초기화 시키는것
    public Vector2 dir { get; private set; }//초기화와 대입 public get, private set

    private int pointerId;

    public void Awake() => instance = this;//람다식으로 싱글톤 생성
    public void Start() => StartCoroutine("ResetState", 0);//ResetState 코루틴 실행 계속 입력 받는중

    private IEnumerator ResetState()
    {
        while(true)
        {
            yield return new WaitUntil(() => inputState != InputState.None && inputState != InputState.Touching);//터치나 드래그중, 홀드때가 아닐때는 yield return으로 나가고 아니면 아래 문장 실행
            inputState = InputState.None;//터치중이 아닐때 상태를 none으로 표시
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(pointerId != eventData.pointerId) return;//입력된 값이 다르면 종료
        // Debug.Log("DragBegin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(pointerId != eventData.pointerId) return;//입력된 값이 다르면 종료
        // Debug.Log("Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(pointerId != eventData.pointerId) return;//입력된 값이 다르면 종료
        inputState = InputState.Drag;//입력 상태 드래그로 표시
        dir = eventData.position - eventData.pressPosition;//방향 계산 eventData(PointerEventData 클래스)안에 존재하는 변수
        dir = dir.normalized;
        // Debug.Log("DragEnd");
        // Debug.Log("Dir : " + dir);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(inputState != InputState.None || inputState != InputState.Touching) return;//입력이 있을때 종료
        //입력이 없을때 아래 실행
        inputState = InputState.Touching;//inputState에 입력중으로 표시
        pointerId = eventData.pointerId;//클릭된 객체 가져옴
        dir = Vector2.zero;//방향 벡터 종료
         Debug.Log("Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(pointerId != eventData.pointerId) return;//입력된 값이 다르면 종료
        inputState = InputState.Touch;//상태가 터치로 표시
        // Debug.Log("Up");
    }
}
