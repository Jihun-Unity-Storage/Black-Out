using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CombinedMovementAndSizeChange : MonoBehaviour
{
    public Transform firstTarget;
    public Transform secondTarget;
    public float totalTimeFirstMove = 5f;
    public float totalTimeSecondMove = 5f;
    public float firstTargetSize = 85f;
    public float secondTargetSize = 6f;
    public float distanceThreshold = 1f; // ���ϴ� �Ÿ� �Ӱ谪 ����
    public bool firstelse = true;
    private float startTime;
    private Vector3 velocity = Vector3.zero; // SmoothDamp�� ���Ǵ� ����
    private float currentSize; // ���� ī�޶� ũ��
    private float smoothDampVelocity; // SmoothDamp�� ���Ǵ� ����
    public Image fadeImage;
    public float fadeDuration = 1.5f;
    void Start()
    {
        firstelse = true;
        startTime = Time.time;
        currentSize = Camera.main.orthographicSize;
        StartCoroutine(CombinedMovement());
    }

    // �ڷ�ƾ���� ����
    IEnumerator CombinedMovement()
    {
        while (true)
        {
            float elapsedTime = Time.time - startTime;
            float distanceToTarget = Vector3.Distance(transform.position, firstTarget.position);

            // ù ��° �̵��� ũ�� ����
            if (distanceToTarget > distanceThreshold && firstelse)
            {
                float t = Mathf.Clamp01(elapsedTime / totalTimeFirstMove);

                Vector3 targetPosition = new Vector3(firstTarget.position.x, firstTarget.position.y, transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, totalTimeFirstMove, Mathf.Infinity, Time.deltaTime);

                float smoothSize = Mathf.Lerp(50f, firstTargetSize, t);
                Camera.main.orthographicSize = smoothSize;
            }
            else
            {
                if (firstelse)
                {
                    firstelse = false;
                    totalTimeFirstMove = elapsedTime;
                }
                // �� ��° �̵��� ũ�� ����
                float t = Mathf.Clamp01((elapsedTime - totalTimeFirstMove) / totalTimeSecondMove);
                //Debug.Log(t);

                Vector3 targetPosition = new Vector3(secondTarget.position.x, secondTarget.position.y, transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, totalTimeSecondMove, Mathf.Infinity, Time.deltaTime);

                float smoothSize = Mathf.SmoothDamp(Camera.main.orthographicSize, secondTargetSize, ref smoothDampVelocity, totalTimeSecondMove);
                Camera.main.orthographicSize = smoothSize;

                // Ÿ�ٰ��� �Ÿ��� Ȯ���Ͽ� �� ��° �̵��� �Ϸ�Ǿ����� �˻�
                distanceToTarget = Vector3.Distance(transform.position, secondTarget.position);
                Debug.Log(distanceToTarget);
                if (distanceToTarget <= 11f)
                {
                    // �Ÿ��� ���� �� ������ �� �ݺ� ����
                    Debug.Log("finish");
                    StartCoroutine(FadeOut());
                    break;
                }
            }

            // 1������ ��ٸ���
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // ���� ����

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            Color currentColor = new Color(startColor.r, startColor.g, startColor.b, alpha);
            fadeImage.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �� ��ȯ
        SceneManager.LoadScene("StartScene");
    }

}
