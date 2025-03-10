using UnityEngine;

public class SmoothCameraSizeChange : MonoBehaviour
{
    public float firstTargetSize = 85f; // ù ��° ��ǥ ī�޶� ũ��
    public float secondTargetSize = 6f; // �� ��° ��ǥ ī�޶� ũ��
    public float firstChangeDuration = 0.2f; // ù ��° ũ�� ���濡 �ɸ��� �ð�
    public float delayBeforeSecondChange = 4.5f; // ù ��° ũ�� ������ �Ϸ�� �� �� ��° ũ�� ������ ���۵Ǳ������ ������
    public float secondChangeDuration = 3f; // �� ��° ũ�� ���濡 �ɸ��� �ð�

    private float startSize;
    private float currentVelocity; // SmoothDamp�� ���Ǵ� ����
    private bool hasFirstSizeChangeCompleted = false;
    private float startTimeFirstChange; // startTimeFirstChange ���� ����

    void Start()
    {
        startSize = 50f; // ���� ũ�⸦ 50���� ����
        startTimeFirstChange = Time.time; // startTimeFirstChange �ʱ�ȭ
    }

    void Update()
    {
        if (!hasFirstSizeChangeCompleted)
        {
            UpdateFirstSizeChange();
        }
        else
        {
            // ù ��° ũ�� ������ �Ϸ�� �� delayBeforeSecondChange ��ŭ�� �ð��� ������ �� ��° ũ�� ���� ����
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
        float t = Mathf.Clamp01(currentTimeFirstChange / firstChangeDuration); // 0���� 1 ���̷� ����

        // SmoothDamp�� ����Ͽ� ���� ũ�⿡�� ù ��° ��ǥ ũ��� �ε巴�� ũ�� ����
        float smoothSize = Mathf.SmoothDamp(Camera.main.orthographicSize, firstTargetSize, ref currentVelocity, firstChangeDuration);

        // ī�޶� ũ�� ����
        Camera.main.orthographicSize = smoothSize;

        // ù ��° ũ�� ������ �Ϸ�Ǹ� �÷��� ����
        if (t >= 1.0f)
        {
            hasFirstSizeChangeCompleted = true;
        }
    }

    void UpdateSecondSizeChange()
    {
        // �� ��° ��ǥ ũ��� ���� ����
        float startTimeSecondChange = Time.time; // startTimeSecondChange ���� ����
        float currentTimeSecondChange = Time.time - (startTimeFirstChange + firstChangeDuration + delayBeforeSecondChange);
        float t = Mathf.Clamp01(currentTimeSecondChange / secondChangeDuration); // 0���� 1 ���̷� ����

        // SmoothDamp�� ����Ͽ� ���� ũ�⿡�� �� ��° ��ǥ ũ��� �ε巴�� ũ�� ����
        float smoothSize = Mathf.SmoothDamp(Camera.main.orthographicSize, secondTargetSize, ref currentVelocity, secondChangeDuration);

        // ī�޶� ũ�� ����
        Camera.main.orthographicSize = smoothSize;

        // �� ��° ũ�� ������ �Ϸ�Ǹ� ��ũ��Ʈ ��Ȱ��ȭ
        if (t >= 1.0f)
        {
            enabled = false;
        }
    }
}
