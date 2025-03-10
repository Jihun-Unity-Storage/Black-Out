using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Light_off : MonoBehaviour
{
    public Sprite Switch_pushed;
    public bool isPushed = false;
    public GameObject[] light_squares;
    public GameObject[] lights;


    public GameObject camera;
    public Transform playerPos, InteractionPos;
    private GameObject player;
    public float stayTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        isPushed = false;
        foreach (GameObject light_square in light_squares)
        {
            LightSwitchTick squares_script = light_square.GetComponent<LightSwitchTick>();
            squares_script.on();

        }
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isPushed)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Switch_pushed;
            if (InteractionPos != null)
            {

                StartCoroutine("StartCameraMove");
            }
            isPushed = true;
        }
    }

    IEnumerator StartCameraMove()
    {
        camera.GetComponent<CameraFollow>().target = InteractionPos;
        //cameraShake.StartShake();
        player.GetComponent<Player>().isMusuktime = true;

        yield return new WaitForSeconds(stayTime/2);
        foreach(GameObject light in lights)
        {
            light.SetActive(false);
        }
        foreach(GameObject light_square in light_squares)
        {
            LightSwitchTick squares_script = light_square.GetComponent<LightSwitchTick>();
            squares_script.off();

        }
        yield return new WaitForSeconds(stayTime/2);

        camera.GetComponent<CameraFollow>().target = playerPos;

        yield return new WaitForSeconds(1f);
        player.GetComponent<Player>().isMusuktime = false;
    }
}
