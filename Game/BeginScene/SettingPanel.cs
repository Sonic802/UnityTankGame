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
            //�жϵ�ǰ���ڳ����ķ���
            if(SceneManager.GetActiveScene().name=="BeginScene")
            {
                BeginPanel.Instance.ShowMe();
            }
            
        };

        slidermusic.OnValueChanged += (value) =>
        {
            //�������ֱ仯
            GameDataMgr.Instance.ChangeBgmVolume(value);
        };
        slidersound.OnValueChanged += (value) =>
        {
            //������Ч�仯
            GameDataMgr.Instance.ChangeSoundVolume(value);
        };

        toggleMusic.isPressed += (value) =>
        {
            //���ֿ���
            GameDataMgr.Instance.ToggleMusic(value);
        };

        toggleSound.isPressed += (value) =>
        {
            //��Ч����
            GameDataMgr.Instance.ToggleSound(value);
        };

        HideMe ();
    }

   public void UpdatePanelInfo()
    {
        //�����Ϣ �Ǹ��� ��Ч���ݸ��µ�
        MusicData data = GameDataMgr.Instance.musicData;

        //�����������
        slidermusic.nowValue = data.bgmVolume;
        slidersound.nowValue = data.soundVolume;
        toggleMusic.isSel = data.isOpenBgm;
        toggleSound.isSel = data.isOpenSound;
    }

    public override void ShowMe()
    {
        base.ShowMe();

        //ÿ����ʾ���ʱ ˳�������ϵ����ݸ�����
        UpdatePanelInfo();
    }

    public override void HideMe()
    {
        base.HideMe();

        Time.timeScale = 1.0f;
    }
}
