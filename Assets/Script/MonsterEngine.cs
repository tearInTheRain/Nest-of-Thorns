using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEngine : MonoBehaviour
{
     public GameObject[] normalMonsters;  // 普通怪物的预制体
    public GameObject[] specialMonsters; // 特殊怪物的预制体
    public GameObject eliteMonster;      // 精英怪的预制体

    public float spawnInterval = 5f;    // 刷怪间隔
    public float bossSpawnInterval = 30f; // 精英怪刷新的间隔
    public float floodSpawnInterval = 20f; // 群怪刷新间隔

    private float spawnTimer = 0f;
    private float strengthFactor = 1f;  // 怪物强度因子

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(SpawnMonsters());
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        
        // 随着时间推移增加怪物的强度
        strengthFactor += Time.deltaTime * 0.1f;
    }

     //刷怪循环
    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            if (spawnTimer >= floodSpawnInterval)
            {
               SpawnFloodMonsters();
            }
             
             
            // 刷普通怪物（随着时间推移强度逐渐增加）
            if (spawnTimer >= spawnInterval)
            {
                SpawnNormalMonster();
                spawnTimer = 0f;
            }

            // 定期刷群怪（包围主角）
            if (spawnTimer >= floodSpawnInterval)
            {
               SpawnFloodMonsters();
            }

            // 随机刷特殊怪
            if (Random.Range(0f, 1f) < 0.05f)  // 5% 概率随机刷特殊怪
            {
                SpawnSpecialMonster();
            }

            // 刷精英怪
            if (spawnTimer >= bossSpawnInterval)
            {
                SpawnEliteMonster();
            }

            yield return null;
        }
    }

    void SpawnNormalMonster()
    {
        GameObject monster = normalMonsters[Random.Range(0, normalMonsters.Length)];
        Vector3 spawnPosition = new Vector3(
            Random.Range(-10f, 10f),
            Random.Range(-10f, 10f),
            0f
            
        );
        Instantiate(monster, spawnPosition, Quaternion.identity);
    }

    void SpawnFloodMonsters()
    {
        // 生成怪物包围主角，怪物围成一个圆形
        int numberOfMonsters = 12; // 可以调整怪物的数量
        float radius = 5f; // 包围的半径

        for (int i = 0; i < numberOfMonsters; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfMonsters; // 将角度分成均匀的间隔
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0); // 计算方向
            Vector3 spawnPosition = new Vector3(player.position.x, player.position.z, 0) + direction * radius; // 计算生成位置
            Debug.Log(spawnPosition);
            Instantiate(normalMonsters[Random.Range(0, normalMonsters.Length)], spawnPosition, Quaternion.identity); // 生成怪物
        }
    }

    void SpawnSpecialMonster()
    {
        GameObject specialMonster = specialMonsters[Random.Range(0, specialMonsters.Length)];
        Vector3 spawnPosition = new Vector3(
            Random.Range(-15f, 15f),
            0f,
            Random.Range(-15f, 15f)
        );
        Instantiate(specialMonster, spawnPosition, Quaternion.identity);
    }

    void SpawnEliteMonster()
    {
        Vector3 spawnPosition = player.position + new Vector3(0, 0, 10f);  // 在主角前方生成
        GameObject elite = Instantiate(eliteMonster, spawnPosition, Quaternion.identity);
        // 精英怪可以根据强度因子调整属性
       // elite.GetComponent<Monster>().SetStrength(strengthFactor);
    }
}
