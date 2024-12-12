using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ���ݹ����� ����ģʽ����
/// </summary>
public class GameDataMgr 
{
    //����ģʽ ��Ϸֻ����һ�����ݹ����������һ����Ч���ݺ�һ�����а�����
    private static GameDataMgr instance = new GameDataMgr();

    public static GameDataMgr Instance => instance;

    //��Ч���ݶ���
    public MusicData musicData;

    //���а����ݶ���
    public RankList rankData;

    //���캯��
    private GameDataMgr()
    {
        //��ʼ�� ��ȡ��������
        musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData), "music") as MusicData;

        //��Ϊ��һ����Ϸ û����Ч���� ����ΪĬ��ֵ0/false�����ԣ�
        if (!musicData.notFirst)
        {
            musicData.notFirst = true;
            //��һ�μ��� �ֶ���ʼ��
            musicData.isOpenSound = true;
            musicData.isOpenBgm = true;
            musicData.bgmVolume = 1;
            musicData.soundVolume = 1;
            //��ʼ����㱣������
            PlayerPrefsDataMgr.Instance.SaveData(musicData,"music");
        }

        //��ʼ�� ��ȡ���а�����
        rankData = PlayerPrefsDataMgr.Instance.LoadData(typeof(RankList),"rank") as RankList;


    }

    //�ṩһ�������а��� ������ݵķ���
    public void AddRankInfo(string name,int score,float time)
    {
        rankData.list.Add(new RankInfo(name,score,time));
        //����
        rankData.list.Sort((a,b)=>a.time<b.time ? -1 :1);
        //�Ƴ�3�����������
        for(int i = rankData.list.Count - 1; i >= 3; i--)
        {
            rankData.list.RemoveAt(i);  
        }
        //�洢����
        PlayerPrefsDataMgr.Instance.SaveData(rankData, "rank");
    }

    //������а�����
    //public void RemoveRankInfo()
    //{
    //    rankData.list.Clear();
    //}

    //��Ϊ���ݹ����ࣺ�ṩһЩAPI���ⲿ �������ݵĸı�ʹ洢
    //�翪�����ֵ�
    public void ToggleMusic(bool isOpen)
    {
        //��¼����
        musicData.isOpenBgm = isOpen;

        //���Ƴ����������ֿ��ء�������ֻ�Ǽ�¼�ʹ洢���ݣ���Ҫ��ʵ�ĸı������Ŀ���
        BGMMusic.Instance.ChangeOpen(isOpen);

        //�洢�ı�������
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

        //���Ƴ������ִ�С
        BGMMusic.Instance.ChangeVolume(volume);

        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }

    public void ChangeSoundVolume(float volume)
    {
        musicData.soundVolume= volume;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }
}
