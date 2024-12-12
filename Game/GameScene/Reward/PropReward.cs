using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PropType
{
    Atk,
    Def,
    MaxHp,
    Hp,
}

public class PropReward : MonoBehaviour
{
    public E_PropType type = E_PropType.Atk;

    //奖励特效
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        //只有玩家 才能获取属性奖励
        if (other.CompareTag("Player"))
        {
            //得到对应的玩家脚本
            PlayerObj player = other.GetComponent<PlayerObj>();
            //根据类型加属性
            //switch 配合 枚举变量来使用
            switch (type)
            {
                case E_PropType.Atk:
                    player.atk += 2;
                    break;
                case E_PropType.Def:
                    player.def += 1;
                    break;
                case E_PropType.MaxHp:
                    player.maxHp += 5;
                    //更新血条
                    GamePanel.Instance.UpdateHP(player.maxHp, player.hp);
                    break;
                case E_PropType.Hp:
                    player.hp += 10;
                    //上限
                    if (player.hp > player.maxHp)
                    {
                        player.hp = player.maxHp;
                    }
                    //更新血条
                    GamePanel.Instance.UpdateHP(player.maxHp, player.hp);
                    break;
               
            }
            //播放奖励 特效
            GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
            //控制 获取音效
            AudioSource audioSource = eff.GetComponent<AudioSource>();
            //大小和开启状态
            audioSource.volume = GameDataMgr.Instance.musicData.soundVolume;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            Destroy(this.gameObject); 
        }
    }
}
