using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Weapon 
{
    float getColdDownTime();
    
    float getLastFireTime();

    void setLastFireTime(float time);

    void attack(List<GameObject> list);
}
