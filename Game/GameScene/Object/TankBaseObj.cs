using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBaseObj : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public int atk;
    public int def;

    public float moveSpeed = 10;
    public float rotateSpeed = 100;
    public float headRotateSpeed = 100;

    //所有坦克都有炮台
    public Transform tankHead; 

    //死亡特效 关联对应预设体 死亡的时候动态创建出来 设置位置
    public GameObject deadEffect;

    //开火抽象方法 子类重写即可
    public abstract void Fire();

    //被别人攻击 自己受到伤害
    public virtual void Wound(TankBaseObj other)
    {
        int dmg = other.atk - this.def;
        if (dmg <= 0)
            return;
        //伤害大于0 减血
        this.hp -= dmg;
        //判断坦克是否死亡
        if (this.hp <= 0)
        {
            //避免负数HP
            this.hp = 0;
            Die();
        }

    }

    public virtual void Die()
    {
        //对象死亡 在场景上移除该对象
        Destroy(this.gameObject);
        //播放对应的特效
        if(deadEffect != null)
        {
            //实例化对象,顺便把位置和角度一起设置了
            GameObject effectObj = Instantiate(deadEffect,this.transform.position,this.transform.rotation);
            //由于该特效对象 直接关联了音效 所以可以在此处 把音效播放相关也进行控制
            AudioSource audioSource = effectObj.GetComponent<AudioSource>();
            //根据音乐数据  设置音效大小和是否播放
            audioSource.volume = GameDataMgr.Instance.musicData.soundVolume;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenBgm;
            //避免没有勾选PlayonAwake
            audioSource.Play();
        }
    }
}
