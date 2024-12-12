using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTank : TankBaseObj
{
    //1.̹�� ��������֮�� �����ƶ�
    //��ǰ��Ŀ���
    private Transform targetPos;

    //����õĵ� ����ȥ���� 
    public Transform[] randomPos;

    //2.̹��һֱ�������̹��
    public Transform lookAtTarget;

    //3.�������̹�˾���С��һ������� ���һ��ʱ�� �������
    //�������
    public float fireDis = 5;
    //�������ʱ��
    public float fireInterval = 1.5f;
    private float nowTime;
    //����λ��
    public Transform shootPos;

    //�ӵ�Ԥ���� ����
    public GameObject bulletObj;

    //չʾѪ����ʣ��ʱ�� Ĭ��Ϊ0 ��չʾ �����˲�����
    private float showTime;

    //Ѫ����ͼ ����
    public Texture maxHpBack;
    public Texture nowHp;

    //RectΪ�ṹ�� ����new
    private Rect maxHpRect;
    private Rect nowHpRect;

    // Start is called before the first frame update
    void Start()
    {
        //һ��ʼ �������Ŀ���
        RandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        //�����֮�������ƶ�:
        //�����Լ���Ŀ���
        this.transform.LookAt(targetPos);
        //��ͣ�����Լ����泯��λ��
        this.transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime);

        //֪ʶ:Vector3 �õ�������֮�����ķ���
        //�� ��Ŀ���ľ���С��0.05ʱ ��Ϊ������ �������ȡһ���� ��ΪĿ���
        
        if (Vector3.Distance(this.transform.position, targetPos.position) < 0.05f)
        {
            RandomPos();
        }

        //�Ƿ���Ҫ����������Ŀ��
        if (lookAtTarget!=null)
        {   //�����Լ���Ŀ��
            tankHead.LookAt(lookAtTarget);
            //�жϺ�Ŀ��֮��ľ���
            if(Vector3.Distance(this.transform.position, lookAtTarget.position) < fireDis)
            {
                //����
                //��ͣ�ۼ�ʱ��
                nowTime += Time.deltaTime;
                //�ۼ�ʱ�� ���� ������ ������
                if (nowTime > fireInterval)
                {
                    Fire();
                    nowTime = 0;
                }
            }


        }
        
    }
    //�������Ŀ���
    private void RandomPos()
    {
        if (randomPos.Length == 0)
            return;
        //������ĵ������ȡһ������ ��ΪĿ���
        targetPos = randomPos[Random.Range(0, randomPos.Length)];
    }

    public override void Fire()
    {
        //ʵ�����ӵ�
        GameObject obj = Instantiate(bulletObj, shootPos.position, shootPos.rotation);
        //�����ӵ��ķ����� ������������Լ���
        BulletObj bullet = obj.GetComponent<BulletObj>();
        bullet.SetFather(this);
    }

    public override void Die()
    {
        base.Die();
        GamePanel.Instance.AddScore(10);
    }

    //Ѫ��UI�Ļ���
    private void OnGUI()
    {
        if (showTime > 0)
        {
            //ÿ�ν��붼��ȥ֡���ʱ�� showtime����0������ʾ
            showTime -= Time.deltaTime;

            //��Ѫ��
            //1.�ѹ��ﵱǰλ��ת��Ϊ ��Ļλ��
            //Camera���ṩ��API
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            //2.��Ļλ�� ת��Ϊ GUIλ��
            //֪ʶ: ��εõ���ǰ��Ļ�ķֱ��ʵĸ�
            //��Ļ����ϵ������Ϊԭ��,GUI����ϵ������Ϊԭ��,����Ҫ��ȥ
            screenPos.y = Screen.height - screenPos.y;

            //�ٻ���
            //֪ʶ:GUIͼƬ����
            //�Ȼ���ͼ
            //�ӵ�ǰλ��������ƫ��50������
            maxHpRect.x = screenPos.x - 50;
            maxHpRect.y = screenPos.y - 50;
            maxHpRect.width = 100;
            maxHpRect.height = 15;
            GUI.DrawTexture(maxHpRect, maxHpBack);
            //�ٻ���ǰѪ��
            nowHpRect.x = screenPos.x - 50;
            nowHpRect.y = screenPos.y - 50;
            //����Ѫ���͵�ǰѪ���ı�ֵ ������� �ǵ�Ҫ������һ��intǿתfloat
            nowHpRect.width = (float)hp / maxHp * 100;
            nowHpRect.height = 15;
            GUI.DrawTexture(nowHpRect, nowHp);
        }
        

    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //���˺�,ȥ����չʾѪ����ʱ��Ϊ3��
        showTime = 3;

    }
}
