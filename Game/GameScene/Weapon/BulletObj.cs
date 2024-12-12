using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    public float moveSpeed = 50;
    //˭����Ĵ��ӵ�
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

    //�ӵ��ͱ�����ײ����ʱ�Ĵ���
    private void OnTriggerEnter(Collider other)
    {
        //���������� ��ը
        //���ӵ������ ��ͬ��Ӫ�Ķ��� Ҳ��ը
        
        if (other.CompareTag("Cube")||
            other.CompareTag("Monster")&&fatherObj.CompareTag("Player")||
            other.CompareTag("Player") && fatherObj.CompareTag("Monster"))
        {

            //�ж��Ƿ�����
            //�õ���ײ�Ķ�������  �Ƿ���̹����ؽű� �������滻ԭ��
            //ͨ������ȥ��ȡ(�ص�!)
            TankBaseObj obj =other.GetComponent<TankBaseObj>();
            if (obj != null)
            {
                obj.Wound(fatherObj);
            }

            //�ӵ�����ʱ ��ӱ�ը��Ч
            if(effObj != null)
            {   //������ը����
                GameObject eff =Instantiate(effObj,transform.position,transform.rotation);
                //�޸���Ч�������Ϳ���״̬
                AudioSource audioSource = eff.GetComponent<AudioSource>();
                //��������
                audioSource.volume = GameDataMgr.Instance.musicData.soundVolume;
                //�����Ƿ��� 
                audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            }
            //�����ӵ�
            Destroy(gameObject);
        }
        
    }
     
    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }
}
