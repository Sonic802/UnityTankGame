using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObj : MonoBehaviour
{
    //奖励物品预设体 关联
    public GameObject[] rewardObjects;

    //破坏特效
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        //1.打中自己的子弹 应销毁
        //只需要更改箱子的tag改为cube即可 以前处理过打中cube销毁自己的逻辑

        //2.打中自己 随机创建奖励的逻辑 左包含 右不包含 0~99 一百个数
        int randomNum = Random.Range(0, 100);
        if(randomNum <50 )
        {
            //随机创建一个奖励预设体在当前位置 0~4 五种奖励
            randomNum = Random.Range(0, rewardObjects.Length);
            //放在当前箱子的位置即可
            Instantiate(rewardObjects[randomNum], this.transform.position, this.transform.rotation);
             
        }

        //播放破坏 特效
        GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
        //控制 获取音效
        AudioSource audioSource = eff.GetComponent<AudioSource>();
        //大小和开启状态
        audioSource.volume = GameDataMgr.Instance.musicData.soundVolume;
        audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;

        Destroy(gameObject);
    }
}
