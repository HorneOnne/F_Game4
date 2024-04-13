using System;
using UnityEngine.UI;

public class UIWelcome : CustomCanvas
{
    
    public Button OkBtn;

    private void Start()
    {
        OkBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            UIManager.Instance.CloseAll();
            UIManager.Instance.DisplayUIChooseTheme(true);
        });
    }

    private void OnDestroy()
    {
        OkBtn.onClick.RemoveAllListeners();
    }
}
