using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : BasePanel<LosePanel>
{
    public CustomGUIButton btnRestart;
    public CustomGUIButton btnQuit;

    private void Start()
    {
        btnRestart.clickEvent += () =>
        {
            //ȡ����ͣ
            Time.timeScale = 1;
            SceneManager.LoadScene("GameScene");
        };

        btnQuit.clickEvent += () =>
        {
            //ȡ����ͣ
            Time.timeScale = 1;
            SceneManager.LoadScene("BeginScene");
        };

        HideMe();
    }
}
