using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏数据管理类 单例模式对象
/// </summary>
public class GameDataMgr 
{
    //单例模式 游戏只存在一个数据管理器，存放一份音效数据和一份排行榜数据
    private static GameDataMgr instance = new GameDataMgr();

    public static GameDataMgr Instance => instance;

    //音效数据对象
    public MusicData musicData;

    //排行榜数据对象
    public RankList rankData;

    //构造函数
    private GameDataMgr()
    {
        //初始化 读取音乐数据
        musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData), "music") as MusicData;

        //因为第一次游戏 没有音效数据 数据为默认值0/false，所以：
        if (!musicData.notFirst)
        {
            musicData.notFirst = true;
            //第一次加载 手动初始化
            musicData.isOpenSound = true;
            musicData.isOpenBgm = true;
            musicData.bgmVolume = 1;
            musicData.soundVolume = 1;
            //初始化后便保存数据
            PlayerPrefsDataMgr.Instance.SaveData(musicData,"music");
        }

        //初始化 读取排行榜数据
        rankData = PlayerPrefsDataMgr.Instance.LoadData(typeof(RankList),"rank") as RankList;


    }

    //提供一个在排行榜中 添加数据的方法
    public void AddRankInfo(string name,int score,float time)
    {
        rankData.list.Add(new RankInfo(name,score,time));
        //排序
        rankData.list.Sort((a,b)=>a.time<b.time ? -1 :1);
        //移除3条以外的数据
        for(int i = rankData.list.Count - 1; i >= 3; i--)
        {
            rankData.list.RemoveAt(i);  
        }
        //存储数据
        PlayerPrefsDataMgr.Instance.SaveData(rankData, "rank");
    }

    //清除排行榜数据
    //public void RemoveRankInfo()
    //{
    //    rankData.list.Clear();
    //}

    //作为数据管理类：提供一些API给外部 方便数据的改变和存储
    //如开关音乐等
    public void ToggleMusic(bool isOpen)
    {
        //记录数据
        musicData.isOpenBgm = isOpen;

        //控制场景背景音乐开关——不能只是记录和存储数据，还要切实的改变音量的开关
        BGMMusic.Instance.ChangeOpen(isOpen);

        //存储改变后的数据
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }

    public void ToggleSound(bool isOpen)
    {
        musicData.isOpenSound = isOpen;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }

    public void ChangeBgmVolume(float volume)
    {
        musicData.bgmVolume = volume;

        //控制场景音乐大小
        BGMMusic.Instance.ChangeVolume(volume);

        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }

    public void ChangeSoundVolume(float volume)
    {
        musicData.soundVolume= volume;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }
}
