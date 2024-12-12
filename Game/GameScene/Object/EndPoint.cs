using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //通关逻辑 暂停 展示胜利界面
            Time.timeScale = 0;
            WinPanel.Instance.ShowMe();
        }
    }
}
