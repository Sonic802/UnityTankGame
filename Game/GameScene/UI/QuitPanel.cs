using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : BasePanel<QuitPanel>
{
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnBack;

    private void Start()
    {
        btnQuit.clickEvent += () =>
        {
            SceneManager.LoadScene("BeginScene");
            
        };

        btnBack.clickEvent += () =>
        {
            HideMe();
            GamePanel.Instance.ShowMe();
            
        };

        HideMe();
    }

    public override void HideMe()
    {
        base.HideMe();

        Time.timeScale = 1.0f;
    }
}
