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

    //����̹�˶�����̨
    public Transform tankHead; 

    //������Ч ������ӦԤ���� ������ʱ��̬�������� ����λ��
    public GameObject deadEffect;

    //������󷽷� ������д����
    public abstract void Fire();

    //�����˹��� �Լ��ܵ��˺�
    public virtual void Wound(TankBaseObj other)
    {
        int dmg = other.atk - this.def;
        if (dmg <= 0)
            return;
        //�˺�����0 ��Ѫ
        this.hp -= dmg;
        //�ж�̹���Ƿ�����
        if (this.hp <= 0)
        {
            //���⸺��HP
            this.hp = 0;
            Die();
        }

    }

    public virtual void Die()
    {
        //�������� �ڳ������Ƴ��ö���
        Destroy(this.gameObject);
        //���Ŷ�Ӧ����Ч
        if(deadEffect != null)
        {
            //ʵ��������,˳���λ�úͽǶ�һ��������
            GameObject effectObj = Instantiate(deadEffect,this.transform.position,this.transform.rotation);
            //���ڸ���Ч���� ֱ�ӹ�������Ч ���Կ����ڴ˴� ����Ч�������Ҳ���п���
            AudioSource audioSource = effectObj.GetComponent<AudioSource>();
            //������������  ������Ч��С���Ƿ񲥷�
            audioSource.volume = GameDataMgr.Instance.musicData.soundVolume;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenBgm;
            //����û�й�ѡPlayonAwake
            audioSource.Play();
        }
    }
}
