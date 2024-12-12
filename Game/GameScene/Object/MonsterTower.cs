using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTower : TankBaseObj
{
    //自动旋转已挂载脚本

    //间隔开火时间
    public float fireInterval = 1;
    private float nowTime = 0;

    //发射位置
    public Transform[] shootPos;

    //子弹预设体 关联
    public GameObject bulletObj;

    private void Update()
    {
        //不停累加时间
        nowTime += Time.deltaTime;
        //累加时间 大于 开火间隔 即开火
        if(nowTime > fireInterval)
        {
            Fire();
            nowTime = 0;
        }
    }


    public override void Fire()
    {
        for (int i = 0; i < shootPos.Length; i++)
        {
            //实例化几个子弹
            GameObject obj = Instantiate(bulletObj, shootPos[i].position, shootPos[i].rotation);
            //设置子弹的发射者 方便后续用属性计算
            BulletObj bullet = obj.GetComponent<BulletObj>();            
            bullet.SetFather(this);
        }
    }

    
    public override void Wound(TankBaseObj other)
    {
        //固定塔不会受到伤害
    }
}
