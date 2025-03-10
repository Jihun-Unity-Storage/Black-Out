using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingText : MonoBehaviour
{
    public Text textPrefab;  // ����� UI Text ������
    public Transform playerTransform;  // �÷��̾��� Transform
    public float fadeDuration = 2f;  // ������� �� �ɸ��� �ð�
    private Canvas canvas;
    private Text floatingText;
    private Coroutine fadeOutCoroutine;

    void Start()
    {
        // UI Text�� �����ϰ� Canvas�� Overlay ���� ����
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

    // ������ ������� �ִϸ��̼� �ڷ�ƾ
    IEnumerator FadeOut(float duration)
    {
        float startTime = Time.time;
        Color originalColor = floatingText.color;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            if (floatingText != null)  // null üũ �߰�
            {
                floatingText.color = Color.Lerp(originalColor, Color.clear, t);
            }
            yield return null;
        }

        // �ִϸ��̼� �Ϸ� �� �ؽ�Ʈ ����
        if (floatingText != null)
        {
            Destroy(floatingText.gameObject);
        }

        fadeOutCoroutine = null; // Coroutine�� ����Ǿ����� ��Ÿ���� ���� ���� �ʱ�ȭ
    }

    public void make_text()
    {
        if (fadeOutCoroutine == null) // FadeOut Coroutine�� ���� ������ ���� ���� ���ο� �ؽ�Ʈ ����
        {
            floatingText = Instantiate(textPrefab, canvas.transform);
            floatingText.text = "�̰Թ���...";
            fadeOutCoroutine = StartCoroutine(FadeOut(fadeDuration));
        }
    }

    void Update()
    {
        // �÷��̾ ����ٴϵ��� ������Ʈ
        if (playerTransform != null && floatingText != null)  // null üũ �߰�
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
