using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : TankBaseObj
{
    //当前的武器
    public WeaponObj nowWeapon;
    //武器父对象位置,用于创建武器
    public Transform weaponPos;

    // Update is called once per frame
    void Update()
    {
        //1.WS键 控制前进
        
        //利用轴向输入检测
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"));

        //2.AD键 控制旋转
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime*Input.GetAxis("Horizontal"));
        //3.鼠标左右移动 控制炮台旋转
        tankHead.Rotate(Vector3.up * headRotateSpeed * Time.deltaTime*Input.GetAxis("Mouse X"));
        //4.开火
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }


    //重写父类中的行为 
    public override void Fire()
    {
        if(nowWeapon != null)
        {
            nowWeapon.Fire();
        }
    }

    public override void Die()
    {
        //不继承基类的死亡方法 因为会把作为子物体的摄像机一起移除 会出问题
        //base.Die();

        //播放对应的特效
        if (deadEffect != null)
        {
            //实例化对象,顺便把位置和角度一起设置了
            GameObject effectObj = Instantiate(deadEffect, this.transform.position, this.transform.rotation);
            //由于该特效对象 直接关联了音效 所以可以在此处 把音效播放相关也进行控制
            AudioSource audioSource = effectObj.GetComponent<AudioSource>();
            //根据音乐数据  设置音效大小和是否播放
            audioSource.volume = GameDataMgr.Instance.musicData.soundVolume;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenBgm;
            //避免没有勾选PlayonAwake
            audioSource.Play();
        }

        // 使用 Invoke 在两秒后调用 ShowLosePanel 方法
        Invoke("ShowLosePanel", 0.5f); // 2f 表示两秒后执行
    }

    private void ShowLosePanel()
    {
        // 暂停游戏
        Time.timeScale = 0;

        // 显示失败面板
        LosePanel.Instance.ShowMe();
    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //更新主面板血条
        GamePanel.Instance.UpdateHP(this.maxHp, this.hp);
    }

    //切换武器
    public void ChangeWeapon(GameObject weapon)
    {
        //删除当前武器
        if(nowWeapon != null)
        {
            Destroy(nowWeapon.gameObject);
            nowWeapon = null;
        }

        //切换武器
        //创建出武器 参数2:设置它的父对象 参数3:保证缩放正常
        GameObject weaponObj = Instantiate(weapon,weaponPos,false);
        nowWeapon =weaponObj.GetComponent<WeaponObj>();
        //设置武器拥有者
        nowWeapon.SetFather(this);
    }
}
