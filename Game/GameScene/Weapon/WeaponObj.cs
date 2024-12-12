using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObj : MonoBehaviour
{
    //用于实例化的子弹对象 
    public GameObject bullet;

    //外部决定有几个发射位置
    public Transform[] shootPos;

    //武器的拥有者
    public TankBaseObj fatherObj;

    //设置拥有者
    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }


    public void Fire()
    {
        //根据位置 创建对应个数子弹
        for (int i = 0; i < shootPos.Length; i++)
        {
            //创建子弹预设体
            GameObject obj = Instantiate(bullet, shootPos[i].position, shootPos[i].rotation);
            //控制子弹行为
            //间接的去设置子弹的发射坦克
            BulletObj bulletObj =obj.GetComponent<BulletObj>();
            bulletObj.SetFather(fatherObj);
        }
    }
}
