using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObj : MonoBehaviour
{
    //������ƷԤ���� ����
    public GameObject[] rewardObjects;

    //�ƻ���Ч
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        //1.�����Լ����ӵ� Ӧ����
        //ֻ��Ҫ�������ӵ�tag��Ϊcube���� ��ǰ���������cube�����Լ����߼�

        //2.�����Լ� ��������������߼� ����� �Ҳ����� 0~99 һ�ٸ���
        int randomNum = Random.Range(0, 100);
        if(randomNum <50 )
        {
            //�������һ������Ԥ�����ڵ�ǰλ�� 0~4 ���ֽ���
            randomNum = Random.Range(0, rewardObjects.Length);
            //���ڵ�ǰ���ӵ�λ�ü���
            Instantiate(rewardObjects[randomNum], this.transform.position, this.transform.rotation);
             
        }

        //�����ƻ� ��Ч
        GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
        //���� ��ȡ��Ч
        AudioSource audioSource = eff.GetComponent<AudioSource>();
        //��С�Ϳ���״̬
        audioSource.volume = GameDataMgr.Instance.musicData.soundVolume;
        audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;

        Destroy(gameObject);
    }
}
