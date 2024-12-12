using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音效数据类
/// </summary>
public class MusicData 
{

    public bool isOpenBgm;
    public bool isOpenSound;

    public float bgmVolume;
    public float soundVolume;

    // 是否是第一次加载的标识 默认false→第一次加载
    public bool notFirst;
}
