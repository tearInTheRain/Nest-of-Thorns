using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour, Weapon
{   
    private float lastFireTime;

    private float coldDownTime = 0.01f;

    public GameObject projectilePrefab; // 投射物预制件
    public float attackRange = 100f; // 查找目标的最大距离

    // 攻击方法
    public void attack(List<GameObject> list)
    {
        if (list == null || list.Count == 0)
        {
            return;
        }

        // 查找最近的目标
        GameObject closestTarget = FindClosestTarget(list);

        if (closestTarget != null)
        {
            // 创建投射物并设置目标
            CreateProjectile(closestTarget);
        }
        else
        {

        }
    }

    // 查找最近的目标
    private GameObject FindClosestTarget(List<GameObject> list)
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;  // 无限大的初始距离

        foreach (var target in list)
        {
            if (target == null)
                continue;

            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < closestDistance && distance <= attackRange)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    // 创建投射物并设置目标
    private void CreateProjectile(GameObject target)
    {
        if (projectilePrefab != null)
        {
            // 实例化投射物
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // 获取投射物组件并设置目标
            Rock projectileScript = projectile.GetComponent<Rock>();
            if (projectileScript != null)
            {
                projectileScript.target = target.transform; // 设置目标为最近的目标
            }
            else
            {

            }
        }
        else
        {

        }
    }

    public float getColdDownTime()
    {
        return coldDownTime;
    }

    public float getLastFireTime()
    {
        return lastFireTime;
    }

    public void setLastFireTime(float time)
    {
        this.lastFireTime = time;
    }
}
