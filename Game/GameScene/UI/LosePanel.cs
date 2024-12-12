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
            //取消暂停
            Time.timeScale = 1;
            SceneManager.LoadScene("GameScene");
        };

        btnQuit.clickEvent += () =>
        {
            //取消暂停
            Time.timeScale = 1;
            SceneManager.LoadScene("BeginScene");
        };

        HideMe();
    }
}
