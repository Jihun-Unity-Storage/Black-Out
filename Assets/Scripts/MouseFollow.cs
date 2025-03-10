using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float mousemoveSpeed = 5.0f; // ������Ʈ�� �̵� �ӵ�
    public float arrowmoveSpeed = 5.0f;

    public bool isOn = true;
    void Update()
    {
        if(!isOn) return;
        
        if (GameManager.lightType == "mouse")
        {
            // ���콺 Ŀ�� ��ġ�� �����ɴϴ�.
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Z �� ���� �����Ͽ� ���ϴ� ���̷� ������ �� �ֽ��ϴ�.
            mousePosition.z = 0;

            // ������Ʈ�� ���콺 ��ġ�� �ε巴�� �̵���ŵ�ϴ�.
            transform.position = Vector3.Lerp(transform.position, mousePosition, mousemoveSpeed * Time.deltaTime);
        }
        else if (GameManager.lightType == "arrow")
        {
            if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(Vector3.up * arrowmoveSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.DownArrow)) transform.Translate(Vector3.down * arrowmoveSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(Vector3.right * arrowmoveSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(Vector3.left * arrowmoveSpeed * Time.deltaTime);

        }
        //else Debug.Log($"현재 타입은 {GameManager.lightType}입니다. 제대로 설정해주세요.");
    }
}
