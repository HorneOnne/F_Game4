using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMainmenu : CustomCanvas
{
    public Button PlayBtn;
    public Button SettingsBtn;
    public Button QuitBtn;

    private void Start()
    {
        GameManager.OnThemeChanged += ChangeTheme;

        PlayBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            Loader.Load(Loader.Scene.GameplayScene);
        });

        SettingsBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            UIManager.Instance.CloseAll();
            UIManager.Instance.DisplaySettingsMenu(true);
        });


        QuitBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            Application.Quit();

            // For the Unity Editor, this will not quit the application. It will stop the editor's play mode.
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        });

        ChangeTheme(GameManager.Instance.CurrentTheme);
    }

    private void ChangeTheme(ThemeDataSO data)
    {
        PlayBtn.image.sprite = data.MenuBtn;
        SettingsBtn.image.sprite = data.MenuBtn;
        QuitBtn.image.sprite = data.MenuBtn;
    }

    private void OnDestroy()
    {
        PlayBtn.onClick.RemoveAllListeners();
        SettingsBtn.onClick.RemoveAllListeners();
        QuitBtn.onClick.RemoveAllListeners();

        GameManager.OnThemeChanged -= ChangeTheme;
    }
}
