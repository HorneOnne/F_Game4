using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class UIGameplay : CustomCanvas
{
    public Image Panel;
    public Button SettingsBtn;
    public Button ChangeThemeBtn;


    private void Start()
    {
        GameManager.OnThemeChanged += UpdateTheme;
        UpdateTheme(GameManager.Instance.CurrentTheme);
        SettingsBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            UIGameplayManager.Instance.DisplayUIGameplaySettings(true);
        });

        ChangeThemeBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            UIGameplayManager.Instance.DisplayUIGameplayChooseTheme(true);
        });

    }


    private void OnDestroy()
    {
        SettingsBtn.onClick.RemoveAllListeners();
        ChangeThemeBtn.onClick.RemoveAllListeners();

        GameManager.OnThemeChanged -= UpdateTheme;
    }

    private void UpdateTheme(ThemeDataSO data)
    {
        Panel.sprite = data.GameplayPanel;
        SettingsBtn.image.sprite = data.GameplaySettingsBtn;
        ChangeThemeBtn.image.sprite = data.GameplayChangeThemeBtn;
    }

}
