using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTower : TankBaseObj
{
    //�Զ���ת�ѹ��ؽű�

    //�������ʱ��
    public float fireInterval = 1;
    private float nowTime = 0;

    //����λ��
    public Transform[] shootPos;

    //�ӵ�Ԥ���� ����
    public GameObject bulletObj;

    private void Update()
    {
        //��ͣ�ۼ�ʱ��
        nowTime += Time.deltaTime;
        //�ۼ�ʱ�� ���� ������ ������
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
            //ʵ���������ӵ�
            GameObject obj = Instantiate(bulletObj, shootPos[i].position, shootPos[i].rotation);
            //�����ӵ��ķ����� ������������Լ���
            BulletObj bullet = obj.GetComponent<BulletObj>();            
            bullet.SetFather(this);
        }
    }

    
    public override void Wound(TankBaseObj other)
    {
        //�̶��������ܵ��˺�
    }
}
