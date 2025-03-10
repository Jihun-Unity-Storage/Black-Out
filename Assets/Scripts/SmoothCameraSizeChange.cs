using UnityEngine;

public class SmoothCameraSizeChange : MonoBehaviour
{
    public float firstTargetSize = 85f; // 첫 번째 목표 카메라 크기
    public float secondTargetSize = 6f; // 두 번째 목표 카메라 크기
    public float firstChangeDuration = 0.2f; // 첫 번째 크기 변경에 걸리는 시간
    public float delayBeforeSecondChange = 4.5f; // 첫 번째 크기 변경이 완료된 후 두 번째 크기 변경이 시작되기까지의 딜레이
    public float secondChangeDuration = 3f; // 두 번째 크기 변경에 걸리는 시간

    private float startSize;
    private float currentVelocity; // SmoothDamp에 사용되는 변수
    private bool hasFirstSizeChangeCompleted = false;
    private float startTimeFirstChange; // startTimeFirstChange 변수 정의

    void Start()
    {
        startSize = 50f; // 시작 크기를 50으로 설정
        startTimeFirstChange = Time.time; // startTimeFirstChange 초기화
    }

    void Update()
    {
        if (!hasFirstSizeChangeCompleted)
        {
            UpdateFirstSizeChange();
        }
        else
        {
            // 첫 번째 크기 변경이 완료된 후 delayBeforeSecondChange 만큼의 시간이 지나면 두 번째 크기 변경 시작
            float elapsedTimeAfterFirstChange = Time.time - (startTimeFirstChange + firstChangeDuration);
            if (elapsedTimeAfterFirstChange >= delayBeforeSecondChange)
            {
                UpdateSecondSizeChange();
            }
        }
    }

    void UpdateFirstSizeChange()
    {
        float currentTimeFirstChange = Time.time - startTimeFirstChange;
        float t = Mathf.Clamp01(currentTimeFirstChange / firstChangeDuration); // 0에서 1 사이로 보간

        // SmoothDamp을 사용하여 현재 크기에서 첫 번째 목표 크기로 부드럽게 크기 변경
        float smoothSize = Mathf.SmoothDamp(Camera.main.orthographicSize, firstTargetSize, ref currentVelocity, firstChangeDuration);

        // 카메라 크기 변경
        Camera.main.orthographicSize = smoothSize;

        // 첫 번째 크기 변경이 완료되면 플래그 설정
        if (t >= 1.0f)
        {
            hasFirstSizeChangeCompleted = true;
        }
    }

    void UpdateSecondSizeChange()
    {
        // 두 번째 목표 크기로 선형 보간
        float startTimeSecondChange = Time.time; // startTimeSecondChange 변수 정의
        float currentTimeSecondChange = Time.time - (startTimeFirstChange + firstChangeDuration + delayBeforeSecondChange);
        float t = Mathf.Clamp01(currentTimeSecondChange / secondChangeDuration); // 0에서 1 사이로 보간

        // SmoothDamp을 사용하여 현재 크기에서 두 번째 목표 크기로 부드럽게 크기 변경
        float smoothSize = Mathf.SmoothDamp(Camera.main.orthographicSize, secondTargetSize, ref currentVelocity, secondChangeDuration);

        // 카메라 크기 변경
        Camera.main.orthographicSize = smoothSize;

        // 두 번째 크기 변경이 완료되면 스크립트 비활성화
        if (t >= 1.0f)
        {
            enabled = false;
        }
    }
}
