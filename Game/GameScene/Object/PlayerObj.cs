using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : TankBaseObj
{
    //��ǰ������
    public WeaponObj nowWeapon;
    //����������λ��,���ڴ�������
    public Transform weaponPos;

    // Update is called once per frame
    void Update()
    {
        //1.WS�� ����ǰ��
        
        //��������������
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"));

        //2.AD�� ������ת
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime*Input.GetAxis("Horizontal"));
        //3.��������ƶ� ������̨��ת
        tankHead.Rotate(Vector3.up * headRotateSpeed * Time.deltaTime*Input.GetAxis("Mouse X"));
        //4.����
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }


    //��д�����е���Ϊ 
    public override void Fire()
    {
        if(nowWeapon != null)
        {
            nowWeapon.Fire();
        }
    }

    public override void Die()
    {
        //���̳л������������ ��Ϊ�����Ϊ������������һ���Ƴ� �������
        //base.Die();

        //���Ŷ�Ӧ����Ч
        if (deadEffect != null)
        {
            //ʵ��������,˳���λ�úͽǶ�һ��������
            GameObject effectObj = Instantiate(deadEffect, this.transform.position, this.transform.rotation);
            //���ڸ���Ч���� ֱ�ӹ�������Ч ���Կ����ڴ˴� ����Ч�������Ҳ���п���
            AudioSource audioSource = effectObj.GetComponent<AudioSource>();
            //������������  ������Ч��С���Ƿ񲥷�
            audioSource.volume = GameDataMgr.Instance.musicData.soundVolume;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenBgm;
            //����û�й�ѡPlayonAwake
            audioSource.Play();
        }

        // ʹ�� Invoke ���������� ShowLosePanel ����
        Invoke("ShowLosePanel", 0.5f); // 2f ��ʾ�����ִ��
    }

    private void ShowLosePanel()
    {
        // ��ͣ��Ϸ
        Time.timeScale = 0;

        // ��ʾʧ�����
        LosePanel.Instance.ShowMe();
    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //���������Ѫ��
        GamePanel.Instance.UpdateHP(this.maxHp, this.hp);
    }

    //�л�����
    public void ChangeWeapon(GameObject weapon)
    {
        //ɾ����ǰ����
        if(nowWeapon != null)
        {
            Destroy(nowWeapon.gameObject);
            nowWeapon = null;
        }

        //�л�����
        //���������� ����2:�������ĸ����� ����3:��֤��������
        GameObject weaponObj = Instantiate(weapon,weaponPos,false);
        nowWeapon =weaponObj.GetComponent<WeaponObj>();
        //��������ӵ����
        nowWeapon.SetFather(this);
    }
}
