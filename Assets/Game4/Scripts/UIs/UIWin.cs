using System;
using UnityEngine.UI;

public class UIWin : CustomCanvas
{
    public Image Panel;
    public Button NextBtn;
    public Image CollectionIcon;

    private void Start()
    {
        GameManager.OnThemeChanged += UpdateTheme;
        UpdateTheme(GameManager.Instance.CurrentTheme);

        NextBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            //GameManager.Instance.RandomLevel();
            Loader.Load(Loader.Scene.GameplayScene);
        });

        GameplayManager.OnWin += UpdateCollectionIcon;
    }



    private void OnDestroy()
    {
        NextBtn.onClick.RemoveAllListeners();
        GameManager.OnThemeChanged -= UpdateTheme;
        GameplayManager.OnWin -= UpdateCollectionIcon;
    }

    private void UpdateTheme(ThemeDataSO data)
    {
        Panel.sprite = data.WinPanel;
        NextBtn.image.sprite = data.WinNextBtn;


    }

    private void UpdateCollectionIcon()
    {
        CollectionIcon.sprite = GameControllers.Instance.Mahjongs[0].Data.Icon;
    }
}
