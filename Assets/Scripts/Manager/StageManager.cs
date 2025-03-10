using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
   public float spawnTime;
   public GameObject spawnEffect;
   public Transform[] spawnPoint;
   public int monsterCount;
   public int maxmonster;

   private void Start()
   {
      StartCoroutine("SpawnStart");
   }

   private void Update()
   {
      GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
      // 배열의 길이(개수)를 출력합니다.
      monsterCount = monsters.Length;
   }

   IEnumerator SpawnStart()
   {
      while (true)
      {
         yield return new WaitForSeconds(spawnTime);
         if(monsterCount <= maxmonster) 
            Instantiate(spawnEffect, spawnPoint[Random.RandomRange(0, spawnPoint.Length)].
               transform.position, Quaternion.identity);
      }
   }
}
