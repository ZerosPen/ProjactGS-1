using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject PausePanel;
    public GameObject SettingsPanel;
    public bool isPause;

    private void Update()
    {
        UIManager.Instance.isPanelOpen = isPause;
    }

    #region pasue
    public void OpenPasuePanel()
    {
        isPause = true;
        PausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPause = false;
        PausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        PausePanel.SetActive(false);
    }

    public void ExitGame()
    {
        PausePanel.SetActive(false);
    }
    #endregion

    #region Settings

    public void OpenSettingsPanel()
    {
        isPause = true;
        SettingsPanel.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        isPause = false;
        SettingsPanel.SetActive(false);
    }

    #endregion
}
