using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReward : MonoBehaviour
{
    //���������� ��ȡ������Ԥ����
    public GameObject[] weaponObj;

    //������Ч
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //������л�����
            int index = Random.Range(0, weaponObj.Length);
            //weaponObj[index]
            //�õ�ײ������� ���Ϲ��صĽű� Ȼ���������л�����
            PlayerObj player =other.GetComponent<PlayerObj>();
            player.ChangeWeapon(weaponObj[index]); 

            //���Ž��� ��Ч
            GameObject eff = Instantiate(getEff,this.transform.position,this.transform.rotation);
            //���� ��ȡ��Ч
            AudioSource audioSource =eff.GetComponent<AudioSource>();
            //��С�Ϳ���״̬
            audioSource.volume = GameDataMgr.Instance.musicData.soundVolume;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;



            //��ȡ�� �Ƴ��Լ�
            Destroy(gameObject);
        }
    }
}
