using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IEquipable {
 
    void Dress(Equipment equipment);

    /*
     * drop: true 装备自动脱落；false 瞬间切换
     */
    void Undress(string stickmanPart,bool drop = false);

}
