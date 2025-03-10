using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingText : MonoBehaviour
{
    public Text textPrefab;  // 사용할 UI Text 프리팹
    public Transform playerTransform;  // 플레이어의 Transform
    public float fadeDuration = 2f;  // 사라지는 데 걸리는 시간
    private Canvas canvas;
    private Text floatingText;
    private Coroutine fadeOutCoroutine;

    void Start()
    {
        // UI Text를 생성하고 Canvas의 Overlay 모드로 설정
        if (GameObject.Find("Text_Canvas"))
        {
            canvas = GameObject.Find("Text_Canvas").GetComponent<Canvas>();
        }
        else
        {
            canvas = new GameObject("Text_Canvas").AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
        playerTransform = GetComponent<Transform>();
    }

    // 서서히 사라지는 애니메이션 코루틴
    IEnumerator FadeOut(float duration)
    {
        float startTime = Time.time;
        Color originalColor = floatingText.color;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            if (floatingText != null)  // null 체크 추가
            {
                floatingText.color = Color.Lerp(originalColor, Color.clear, t);
            }
            yield return null;
        }

        // 애니메이션 완료 후 텍스트 제거
        if (floatingText != null)
        {
            Destroy(floatingText.gameObject);
        }

        fadeOutCoroutine = null; // Coroutine이 종료되었음을 나타내기 위해 변수 초기화
    }

    public void make_text()
    {
        if (fadeOutCoroutine == null) // FadeOut Coroutine이 실행 중이지 않을 때만 새로운 텍스트 생성
        {
            floatingText = Instantiate(textPrefab, canvas.transform);
            floatingText.text = "이게뭐지...";
            fadeOutCoroutine = StartCoroutine(FadeOut(fadeDuration));
        }
    }

    void Update()
    {
        // 플레이어를 따라다니도록 업데이트
        if (playerTransform != null && floatingText != null)  // null 체크 추가
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(playerTransform.position + Vector3.up * 2f);
            floatingText.transform.position = screenPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gold")
        {
            make_text();
        }
    }
}
