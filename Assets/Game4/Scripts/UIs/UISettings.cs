using UnityEngine.UI;
using UnityEngine;

public class UISettings : CustomCanvas
{
    public Image Panel;
    public Button CloseBtn;
    public Button GalleryBtn;

    public Button SoundBtn;
    public Button MusicBtn;



    private void Start()
    {
        GameManager.OnThemeChanged += ChangeTheme;
        UpdateSoundFXUI();
        UpdateMusicUI();

        CloseBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);

            UIManager.Instance.CloseAll();
            UIManager.Instance.DisplayMainmenu(true);
        });



        SoundBtn.onClick.AddListener(() =>
        {
            ToggleSFX();
            SoundManager.Instance.PlaySound(SoundType.Button, false);
        });

        MusicBtn.onClick.AddListener(() =>
        {
            ToggleMusic();
            SoundManager.Instance.PlaySound(SoundType.Button, false);
        });

        GalleryBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            UIManager.Instance.CloseAll();
            UIManager.Instance.DisplayUIGallery(true);
        });

        ChangeTheme(GameManager.Instance.CurrentTheme);
    }

    private void OnDestroy()
    {
        CloseBtn.onClick.RemoveAllListeners();
        SoundBtn.onClick.RemoveAllListeners();
        MusicBtn.onClick.RemoveAllListeners();

        GalleryBtn.onClick.RemoveAllListeners();

        GameManager.OnThemeChanged -= ChangeTheme;
    }


    private void ToggleSFX()
    {
        SoundManager.Instance.MuteSoundFX(SoundManager.Instance.isSoundFXActive);
        SoundManager.Instance.isSoundFXActive = !SoundManager.Instance.isSoundFXActive;

        UpdateSoundFXUI();
    }


    private void UpdateSoundFXUI()
    {
        if (SoundManager.Instance.isSoundFXActive)
        {
            SoundBtn.image.sprite = GameManager.Instance.CurrentTheme.SoundOn;
        }
        else
        {
            SoundBtn.image.sprite = GameManager.Instance.CurrentTheme.SoundOff;
        }
    }

    private void ToggleMusic()
    {
        SoundManager.Instance.MuteBackground(SoundManager.Instance.isMusicActive);
        SoundManager.Instance.isMusicActive = !SoundManager.Instance.isMusicActive;

        UpdateMusicUI();
    }

    private void UpdateMusicUI()
    {
        if (SoundManager.Instance.isMusicActive)
        {
            MusicBtn.image.sprite = GameManager.Instance.CurrentTheme.SoundOn; 
        }
        else
        {
            MusicBtn.image.sprite = GameManager.Instance.CurrentTheme.SoundOff;
        }
    }


    private void ChangeTheme(ThemeDataSO data)
    {
        Panel.sprite = data.SettingsPanel;
        CloseBtn.image.sprite = data.SettingsOkBtn;
        GalleryBtn.image.sprite = data.SettingsGalleryBtn;

        if(SoundManager.Instance.isSoundFXActive)
        {
            SoundBtn.image.sprite = data.SoundOn;
        }
        else
        {
            SoundBtn.image.sprite = data.SoundOff;
        }

        if (SoundManager.Instance.isMusicActive)
        {
            MusicBtn.image.sprite = data.SoundOn;
        }
        else
        {
            MusicBtn.image.sprite = data.SoundOff;
        }
    }
}
