using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int EnemiesCount;
    public GameObject enemyPrefab;

    public Transform player;

    private float Xpos;
    private float Zpos;
    private void Update()
    {
        EnemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        SpawnEnemies();
    }
    void SpawnEnemies()
    {
        if (EnemiesCount < 10)
        {
            Xpos = UnityEngine.Random.Range(player.position.x - 30f, player.position.x + 30f);
            Zpos = UnityEngine.Random.Range(player.position.y - 30f, player.position.y + 30f);

            GameObject currentEnemy = Instantiate(enemyPrefab, new Vector2(Xpos, Zpos), Quaternion.identity);
            currentEnemy.name = "Enemy" + Convert.ToInt32(Xpos) + " " + Convert.ToInt32(Zpos);
            currentEnemy.GetComponent<Enemy>().FireRate = UnityEngine.Random.Range(30, 120);
            currentEnemy.GetComponent<Enemy>().shotPower = UnityEngine.Random.Range(10, 30);
            currentEnemy.GetComponent<Enemy>().MaxLife = UnityEngine.Random.Range(50, 150);
        }
    }
}
