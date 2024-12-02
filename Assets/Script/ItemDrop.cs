using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public float moveSpeed = 5f;  // 飞向玩家的速度
    private Transform player;      // 玩家位置

    private void Start()
    {
        // 获取玩家对象，假设玩家有Tag = "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // 如果玩家已存在且掉落物未被销毁
        if (player != null)
        {
            // 计算飞向玩家的方向
            Vector3 direction = (player.position - transform.position).normalized;

            // 移动掉落物
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    // 碰撞检测，检查掉落物是否与玩家发生碰撞
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 播放拾取动画或音效（如果有的话）

            // 消失掉落物
            Destroy(gameObject);
        }
    }
}
