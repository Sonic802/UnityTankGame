using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReward : MonoBehaviour
{
    //多个用于随机 获取的武器预设体
    public GameObject[] weaponObj;

    //奖励特效
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //让玩家切换武器
            int index = Random.Range(0, weaponObj.Length);
            //weaponObj[index]
            //得到撞到的玩家 身上挂载的脚本 然后命令其切换武器
            PlayerObj player =other.GetComponent<PlayerObj>();
            player.ChangeWeapon(weaponObj[index]); 

            //播放奖励 特效
            GameObject eff = Instantiate(getEff,this.transform.position,this.transform.rotation);
            //控制 获取音效
            AudioSource audioSource =eff.GetComponent<AudioSource>();
            //大小和开启状态
            audioSource.volume = GameDataMgr.Instance.musicData.soundVolume;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;



            //获取后 移除自己
            Destroy(gameObject);
        }
    }
}
