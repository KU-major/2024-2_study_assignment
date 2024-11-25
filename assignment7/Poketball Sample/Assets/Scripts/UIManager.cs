using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Text MyText;

    Coroutine NowCoroutine;

    void Awake() {
        // MyText를 얻어오고, 내용을 지운다.
        // ---------- TODO ---------- 
        MyText = GetComponent<Text>();
        if (MyText != null)
        {
            MyText.text = ""; // 초기화
        }
        else
        {
            Debug.LogError("Text 컴포넌트를 찾을 수 없습니다. UIManager를 Text가 있는 오브젝트에 붙이세요.");
        }
        // -------------------- 
    }

    public void DisplayText(string text, float duration)
    {
        // NowCoroutine이 있다면 멈추고 새로운 DisplayTextCoroutine을 시작한다.
        // ---------- TODO ---------- 
        if (NowCoroutine != null)
        {
            StopCoroutine(NowCoroutine);
        }
        NowCoroutine = StartCoroutine(DisplayTextCoroutine(text, duration));
        // -------------------- 
    }

    IEnumerator DisplayTextCoroutine(string text, float duration)
    {
        // MyText에 text를 duration초 동안 띄운다.
        // ---------- TODO ---------- 
        if (MyText != null)
        {
            MyText.text = text; // 텍스트 설정
            yield return new WaitForSeconds(duration); // duration 대기
            MyText.text = ""; // 텍스트 초기화
        }
        // -------------------- 
    }
}
