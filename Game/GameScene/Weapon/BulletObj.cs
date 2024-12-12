using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    public float moveSpeed = 50;
    //谁发射的此子弹
    public TankBaseObj fatherObj;

    public GameObject effObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    //子弹和别人碰撞触发时的处理
    private void OnTriggerEnter(Collider other)
    {
        //碰到立方体 爆炸
        //或子弹射击到 不同阵营的对象 也爆炸
        
        if (other.CompareTag("Cube")||
            other.CompareTag("Monster")&&fatherObj.CompareTag("Player")||
            other.CompareTag("Player") && fatherObj.CompareTag("Monster"))
        {

            //判断是否受伤
            //得到碰撞的对象身上  是否有坦克相关脚本 用里氏替换原则
            //通过父类去获取(重点!)
            TankBaseObj obj =other.GetComponent<TankBaseObj>();
            if (obj != null)
            {
                obj.Wound(fatherObj);
            }

            //子弹销毁时 添加爆炸特效
            if(effObj != null)
            {   //创建爆炸特性
                GameObject eff =Instantiate(effObj,transform.position,transform.rotation);
                //修改音效的音量和开启状态
                AudioSource audioSource = eff.GetComponent<AudioSource>();
                //设置音量
                audioSource.volume = GameDataMgr.Instance.musicData.soundVolume;
                //设置是否开启 
                audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            }
            //销毁子弹
            Destroy(gameObject);
        }
        
    }
     
    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }
}
