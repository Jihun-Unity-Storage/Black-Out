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
    public float distanceThreshold = 1f; // 원하는 거리 임계값 설정
    public bool firstelse = true;
    private float startTime;
    private Vector3 velocity = Vector3.zero; // SmoothDamp에 사용되는 변수
    private float currentSize; // 현재 카메라 크기
    private float smoothDampVelocity; // SmoothDamp에 사용되는 변수
    public Image fadeImage;
    public float fadeDuration = 1.5f;
    void Start()
    {
        firstelse = true;
        startTime = Time.time;
        currentSize = Camera.main.orthographicSize;
        StartCoroutine(CombinedMovement());
    }

    // 코루틴으로 변경
    IEnumerator CombinedMovement()
    {
        while (true)
        {
            float elapsedTime = Time.time - startTime;
            float distanceToTarget = Vector3.Distance(transform.position, firstTarget.position);

            // 첫 번째 이동과 크기 변경
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
                // 두 번째 이동과 크기 변경
                float t = Mathf.Clamp01((elapsedTime - totalTimeFirstMove) / totalTimeSecondMove);
                //Debug.Log(t);

                Vector3 targetPosition = new Vector3(secondTarget.position.x, secondTarget.position.y, transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, totalTimeSecondMove, Mathf.Infinity, Time.deltaTime);

                float smoothSize = Mathf.SmoothDamp(Camera.main.orthographicSize, secondTargetSize, ref smoothDampVelocity, totalTimeSecondMove);
                Camera.main.orthographicSize = smoothSize;

                // 타겟과의 거리를 확인하여 두 번째 이동이 완료되었는지 검사
                distanceToTarget = Vector3.Distance(transform.position, secondTarget.position);
                Debug.Log(distanceToTarget);
                if (distanceToTarget <= 11f)
                {
                    // 거리가 일정 값 이하일 때 반복 종료
                    Debug.Log("finish");
                    StartCoroutine(FadeOut());
                    break;
                }
            }

            // 1프레임 기다리기
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // 완전 진함

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            Color currentColor = new Color(startColor.r, startColor.g, startColor.b, alpha);
            fadeImage.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 씬 전환
        SceneManager.LoadScene("StartScene");
    }

}
