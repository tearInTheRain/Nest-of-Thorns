using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEngine : MonoBehaviour
{
    private List<Weapon> weaponList = new List<Weapon>();
    public Camera playerCamera; // 玩家摄像机

    public void Start()
    {
        weaponList.Add(GetComponent<Weapon>());
    }

    public void Update()
    {
        float currentTime = Time.time;

        foreach (Weapon weapon in weaponList)
        {
            if (currentTime - weapon.getLastFireTime() > weapon.getColdDownTime())
            {
                List<GameObject> targetsInView = GetMonstersInView();
                if (targetsInView.Count > 0)
                {

                    weapon.attack(targetsInView);
                    weapon.setLastFireTime(currentTime);
                    
                }
            }
        }
    }

     private List<GameObject> GetMonstersInView()
    {
        List<GameObject> monstersInView = new List<GameObject>();

        // 获取所有有 "Monster" 标签的对象
        GameObject[] allMonsters = GameObject.FindGameObjectsWithTag("Monster");
        
        foreach (GameObject monster in allMonsters)
        {
            // 检查怪物是否在摄像机的视野内
            Vector3 viewportPos = playerCamera.WorldToViewportPoint(monster.transform.position);
            if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
            {
                // 在视野内的怪物
                monstersInView.Add(monster);
            }
        }

        return monstersInView;
    }
}