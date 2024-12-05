using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour, MonsterBase
{
    public int Damage = 10;
    public float moveSpeed = 2f;  // 怪物移动速度

    public GameObject itemDrop;

    private string playerTag = "Player";  // 玩家Tag
    
    private Transform player;  // 玩家对象的Transfor
    private Rigidbody2D rb;  // 怪物的Rigidbody2D


    private int hp = 100;
    private ObjectPool objectPool;

    void Start()
    {
        // 获取玩家对象和必要的组件
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        objectPool = GameObject.FindGameObjectWithTag("pool").GetComponent<ObjectPool>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveTowardPlayer();
    }

    void MoveTowardPlayer()
    {
        // 计算朝向玩家的方向
        Vector2 direction = (player.position - transform.position).normalized;
        
        // 设置怪物移动方向
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

        // 控制动画
        if (direction.x < 0)  // 玩家在左边
        {
            transform.localScale = new Vector3(-1, 1, 1);  // 反转怪物朝左
        }
        else  // 玩家在右边
        {
            transform.localScale = new Vector3(1, 1, 1);  // 反转怪物朝右
        }
    }

    // 当触发器碰到玩家时触发事件
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            // 执行碰撞事件 (例如攻击玩家)
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
            playerData.addHp(-Damage);
        }
    }

    public int addHp(int hp)
    {
        this.hp += hp;
        if(this.hp < 0) {
            this.hp = 0;
        }

        if(this.hp <= 0) {
            objectPool.ReturnToPool("monster", gameObject);

            Instantiate(itemDrop, transform.position, Quaternion.identity);
        }

        return this.hp;
    }
}
