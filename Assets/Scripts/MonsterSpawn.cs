using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject wonGuri;
    private void Start()
    {
        Invoke("MonsterSpawnEnd", 1f);
    }

    public void MonsterSpawnEnd()
    {
        Debug.Log("스폰 성공");
        Instantiate(wonGuri, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
