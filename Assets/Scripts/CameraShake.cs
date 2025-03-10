using System.Collections;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;

    // ��鸲�� ���� �� ���� �ð��� �����ϱ� ���� ������
    public float shakeDuration = 0f;
    public float shakeMagnitude = 0.7f;
    public float dampingSpeed = 1.0f;

    private Vector3 initialPosition;

    void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        initialPosition = cameraTransform.localPosition;
    }

    void Update()
    {

        initialPosition = cameraTransform.localPosition;
        if (shakeDuration > 0)
        {
            cameraTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            //cameraTransform.localPosition = initialPosition;
        }
    }

    // ��鸲 ȿ���� Ȱ��ȭ�ϴ� �޼���
    public void StartShake()
    {

        initialPosition = cameraTransform.localPosition;
        StartCoroutine("wait");

    }

    IEnumerator wait()
    {
        
        yield return new WaitForSeconds(0.2f);
        shakeDuration = 1.8f;
    }


}
