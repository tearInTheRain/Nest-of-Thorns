

using UnityEngine;

public class PlayerData : MonoBehaviour {
    
    public BuffEngine buffEngine;

    private int hp;

    private int level;

    private int curEx;

    private int[] needEx = new int[]{1,2,10,100,1000};


    public int getHp() {
        return hp;
    }

    public void setHp(int hp) {
        this.hp = hp;
    }

    public void addHp(int hp) {
        this.hp += hp;
    }

    public void addEx(int ex) {
        curEx += ex; // 增加当前经验值

        // 检查是否需要升级
        while (curEx >= needEx[level] && level < needEx.Length - 1) {
            curEx -= needEx[level]; 
            level++; 
            Debug.Log("升级");
            buffEngine.levelUp(level);
        }
    }
}