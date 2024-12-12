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
        //得到自己游戏对象挂载的音频源的脚本
        audioSource = GetComponent<AudioSource>();
        //读取设置音量
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
