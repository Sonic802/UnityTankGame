using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMMusic : MonoBehaviour
{
    private static BGMMusic instance;

    public static BGMMusic Instance => instance;

    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        //�õ��Լ���Ϸ������ص���ƵԴ�Ľű�
        audioSource = GetComponent<AudioSource>();
        //��ȡ��������
        ChangeVolume(GameDataMgr.Instance.musicData.bgmVolume);
        ChangeOpen(GameDataMgr.Instance.musicData.isOpenBgm);
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void ChangeOpen(bool isOpen)
    {           
        audioSource.mute = !isOpen; 
    }
}
