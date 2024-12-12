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

    //������Ч
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        //ֻ����� ���ܻ�ȡ���Խ���
        if (other.CompareTag("Player"))
        {
            //�õ���Ӧ����ҽű�
            PlayerObj player = other.GetComponent<PlayerObj>();
            //�������ͼ�����
            //switch ��� ö�ٱ�����ʹ��
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
                    //����Ѫ��
                    GamePanel.Instance.UpdateHP(player.maxHp, player.hp);
                    break;
                case E_PropType.Hp:
                    player.hp += 10;
                    //����
                    if (player.hp > player.maxHp)
                    {
                        player.hp = player.maxHp;
                    }
                    //����Ѫ��
                    GamePanel.Instance.UpdateHP(player.maxHp, player.hp);
                    break;
               
            }
            //���Ž��� ��Ч
            GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
            //���� ��ȡ��Ч
            AudioSource audioSource = eff.GetComponent<AudioSource>();
            //��С�Ϳ���״̬
            audioSource.volume = GameDataMgr.Instance.musicData.soundVolume;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            Destroy(this.gameObject); 
        }
    }
}
