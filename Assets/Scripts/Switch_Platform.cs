using System.Collections;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Switch_Platform : MonoBehaviour
{
    public CameraShake cameraShake;
    public Sprite Switch_pushed;
    public bool isPushed = false;
    public Transform platformToMove; // 이동할 플랫폼
    public Transform startPoint; // 이동 시작 위치
    public Transform endPoint;   // 이동 끝 위치
    public float speed = 2.0f;   // 이동 속도

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
        targetPosition = endPoint.position; // 시작할 때는 끝 위치로 이동
        isPushed = false;
    }

    private void Update()
    {
        // 플랫폼을 이동할 위치로 이동
        if (isPushed)
        {

            platformToMove.position = Vector3.MoveTowards(platformToMove.position, targetPosition, speed * Time.deltaTime);
        }

        // 목표 위치에 도달하면 이동 중지
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
