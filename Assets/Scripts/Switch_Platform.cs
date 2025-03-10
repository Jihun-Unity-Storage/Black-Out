using System.Collections;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Switch_Platform : MonoBehaviour
{
    public CameraShake cameraShake;
    public Sprite Switch_pushed;
    public bool isPushed = false;
    public Transform platformToMove; // �̵��� �÷���
    public Transform startPoint; // �̵� ���� ��ġ
    public Transform endPoint;   // �̵� �� ��ġ
    public float speed = 2.0f;   // �̵� �ӵ�

    private bool movingToEnd = true;
    private Vector3 targetPosition;

    public GameObject camera;
    public Transform playerPos, InteractionPos;
    private GameObject player;
    public float stayTime = 2.0f;
    private void Start()
    {
        player = GameObject.Find("Player");
        startPoint.position = platformToMove.position;
        targetPosition = endPoint.position; // ������ ���� �� ��ġ�� �̵�
        isPushed = false;
    }

    private void Update()
    {
        // �÷����� �̵��� ��ġ�� �̵�
        if (isPushed)
        {

            platformToMove.position = Vector3.MoveTowards(platformToMove.position, targetPosition, speed * Time.deltaTime);
        }

        // ��ǥ ��ġ�� �����ϸ� �̵� ����
        //if (Vector3.Distance(platformToMove.position, targetPosition) < 0.01f)
        //{
        //    movingToEnd = !movingToEnd;
        //    targetPosition = movingToEnd ? endPoint.position : startPoint.position;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isPushed)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Switch_pushed;
            if(InteractionPos != null)
            {

                StartCoroutine("StartCameraMove");
            }
            isPushed=true;
        }
    }

    IEnumerator StartCameraMove()
    {
        camera.GetComponent<CameraFollow>().target = InteractionPos;
        //cameraShake.StartShake();
        player.GetComponent<Player>().isMusuktime = true;
        yield return new WaitForSeconds(stayTime);

        camera.GetComponent<CameraFollow>().target = playerPos;
        
        yield return new WaitForSeconds(1f);
        player.GetComponent<Player>().isMusuktime = false;
    }
}
