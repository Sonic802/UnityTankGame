using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : BasePanel<SettingPanel>
{
    public CustomGUIButton buttonClose;

    public CustomGUIToggle toggleMusic;
    public CustomGUIToggle toggleSound;

    public CustomGUISlider slidermusic;
    public CustomGUISlider slidersound;

    

    void Start()
    {
        buttonClose.clickEvent += () =>
        {
            HideMe();
            //判断当前所在场景的方法
            if(SceneManager.GetActiveScene().name=="BeginScene")
            {
                BeginPanel.Instance.ShowMe();
            }
            
        };

        slidermusic.OnValueChanged += (value) =>
        {
            //处理音乐变化
            GameDataMgr.Instance.ChangeBgmVolume(value);
        };
        slidersound.OnValueChanged += (value) =>
        {
            //处理音效变化
            GameDataMgr.Instance.ChangeSoundVolume(value);
        };

        toggleMusic.isPressed += (value) =>
        {
            //音乐开关
            GameDataMgr.Instance.ToggleMusic(value);
        };

        toggleSound.isPressed += (value) =>
        {
            //音效开关
            GameDataMgr.Instance.ToggleSound(value);
        };

        HideMe ();
    }

   public void UpdatePanelInfo()
    {
        //面板信息 是根据 音效数据更新的
        MusicData data = GameDataMgr.Instance.musicData;

        //设置面板内容
        slidermusic.nowValue = data.bgmVolume;
        slidersound.nowValue = data.soundVolume;
        toggleMusic.isSel = data.isOpenBgm;
        toggleSound.isSel = data.isOpenSound;
    }

    public override void ShowMe()
    {
        base.ShowMe();

        //每次显示面板时 顺便把面板上的内容更新了
        UpdatePanelInfo();
    }

    public override void HideMe()
    {
        base.HideMe();

        Time.timeScale = 1.0f;
    }
}
