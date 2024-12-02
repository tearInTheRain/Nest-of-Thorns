

using UnityEngine;

public class PlayerData : MonoBehaviour {
    
    private int hp;

    private int level;

    private int curEx;

    private int maxEx;


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
   
    }

}