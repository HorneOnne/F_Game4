using System;
using UnityEngine.UI;

public class UIGameplayChooseTheme : CustomCanvas
{
    public Button ChristmastBtn_01;
    public Button ChristmastBtn_02;

    public Button ValentineBtn_01;
    public Button ValentineBtn_02;

    private void Start()
    {
        ChristmastBtn_01.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            GameManager.Instance.SetChristmasTheme();
            UIGameplayManager.Instance.DisplayUIGameplayChooseTheme(false);
        });

        ChristmastBtn_02.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            GameManager.Instance.SetChristmasTheme();
            UIGameplayManager.Instance.DisplayUIGameplayChooseTheme(false);
        });

        ValentineBtn_01.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            GameManager.Instance.SetValentineTheme();
            UIGameplayManager.Instance.DisplayUIGameplayChooseTheme(false);
        });

        ValentineBtn_02.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            GameManager.Instance.SetValentineTheme();
            UIGameplayManager.Instance.DisplayUIGameplayChooseTheme(false);
        });


    }


    private void OnDestroy()
    {
        ChristmastBtn_01.onClick.RemoveAllListeners();
        ChristmastBtn_02.onClick.RemoveAllListeners();
        ValentineBtn_01.onClick.RemoveAllListeners();
        ValentineBtn_02.onClick.RemoveAllListeners();
    }
}
