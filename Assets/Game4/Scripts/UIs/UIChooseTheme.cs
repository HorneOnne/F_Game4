using System;
using UnityEngine.UI;

public class UIChooseTheme : CustomCanvas
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
            UIManager.Instance.CloseAll();
            UIManager.Instance.DisplayMainmenu(true);
        });

        ChristmastBtn_02.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            GameManager.Instance.SetChristmasTheme();
            UIManager.Instance.CloseAll();
            UIManager.Instance.DisplayMainmenu(true);
        });

        ValentineBtn_01.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            GameManager.Instance.SetValentineTheme();
            UIManager.Instance.CloseAll();
            UIManager.Instance.DisplayMainmenu(true);
        });

        ValentineBtn_02.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            GameManager.Instance.SetValentineTheme();
            UIManager.Instance.CloseAll();
            UIManager.Instance.DisplayMainmenu(true);
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
