using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTank : TankBaseObj
{
    //1.坦克 在两个点之间 来回移动
    //当前的目标点
    private Transform targetPos;

    //随机用的点 外面去关联 
    public Transform[] randomPos;

    //2.坦克一直盯着玩家坦克
    public Transform lookAtTarget;

    //3.当和玩家坦克距离小于一定间隔后 间隔一段时间 攻击玩家
    //开火距离
    public float fireDis = 5;
    //攻击间隔时间
    public float fireInterval = 1.5f;
    private float nowTime;
    //发射位置
    public Transform shootPos;

    //子弹预设体 关联
    public GameObject bulletObj;

    //展示血条的剩余时间 默认为0 不展示 受伤了才设置
    private float showTime;

    //血条的图 关联
    public Texture maxHpBack;
    public Texture nowHp;

    //Rect为结构体 不用new
    private Rect maxHpRect;
    private Rect nowHpRect;

    // Start is called before the first frame update
    void Start()
    {
        //一开始 随机生成目标点
        RandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        //多个点之间的随机移动:
        //看向自己的目标点
        this.transform.LookAt(targetPos);
        //不停的向自己的面朝向位移
        this.transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime);

        //知识:Vector3 得到两个点之间距离的方法
        //当 和目标点的距离小于0.05时 认为到达了 重新随机取一个点 作为目标点
        
        if (Vector3.Distance(this.transform.position, targetPos.position) < 0.05f)
        {
            RandomPos();
        }

        //是否有要看向和射击的目标
        if (lookAtTarget!=null)
        {   //看向自己的目标
            tankHead.LookAt(lookAtTarget);
            //判断和目标之间的距离
            if(Vector3.Distance(this.transform.position, lookAtTarget.position) < fireDis)
            {
                //开火
                //不停累加时间
                nowTime += Time.deltaTime;
                //累加时间 大于 开火间隔 即开火
                if (nowTime > fireInterval)
                {
                    Fire();
                    nowTime = 0;
                }
            }


        }
        
    }
    //创造随机目标点
    private void RandomPos()
    {
        if (randomPos.Length == 0)
            return;
        //从随机的点钟随机取一个出来 作为目标点
        targetPos = randomPos[Random.Range(0, randomPos.Length)];
    }

    public override void Fire()
    {
        //实例化子弹
        GameObject obj = Instantiate(bulletObj, shootPos.position, shootPos.rotation);
        //设置子弹的发射者 方便后续用属性计算
        BulletObj bullet = obj.GetComponent<BulletObj>();
        bullet.SetFather(this);
    }

    public override void Die()
    {
        base.Die();
        GamePanel.Instance.AddScore(10);
    }

    //血条UI的绘制
    private void OnGUI()
    {
        if (showTime > 0)
        {
            //每次进入都减去帧间隔时间 showtime减到0后不再显示
            showTime -= Time.deltaTime;

            //画血条
            //1.把怪物当前位置转换为 屏幕位置
            //Camera类提供了API
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            //2.屏幕位置 转换为 GUI位置
            //知识: 如何得到当前屏幕的分辨率的高
            //屏幕坐标系以左下为原点,GUI坐标系以左上为原点,所以要减去
            screenPos.y = Screen.height - screenPos.y;

            //再绘制
            //知识:GUI图片绘制
            //先画底图
            //从当前位置往左上偏移50个像素
            maxHpRect.x = screenPos.x - 50;
            maxHpRect.y = screenPos.y - 50;
            maxHpRect.width = 100;
            maxHpRect.height = 15;
            GUI.DrawTexture(maxHpRect, maxHpBack);
            //再画当前血量
            nowHpRect.x = screenPos.x - 50;
            nowHpRect.y = screenPos.y - 50;
            //根据血量和当前血量的比值 决定宽度 记得要把其中一个int强转float
            nowHpRect.width = (float)hp / maxHp * 100;
            nowHpRect.height = 15;
            GUI.DrawTexture(nowHpRect, nowHp);
        }
        

    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //受伤后,去设置展示血条的时间为3秒
        showTime = 3;

    }
}
