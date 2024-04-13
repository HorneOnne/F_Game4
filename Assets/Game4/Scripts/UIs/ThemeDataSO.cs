using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class ThemeDataSO : ScriptableObject
{
    [Header("Menu")]
    public Sprite MenuBtn;
    public Sprite Background;

    [Header("Settings")]
    public Sprite SettingsPanel;
    public Sprite SettingsGalleryBtn;
    public Sprite SettingsOkBtn;
    public Sprite SoundOn;
    public Sprite SoundOff;

    [Header("Gallery")]
    public Sprite GalleryPanel;
    public Sprite GalleryCloseBtn;
    public Sprite GalleryUnlockSlot;
    public Sprite GalleryLockSlot;


    [Header("Gameplay")]
    public Sprite GameplayPanel;
    public Sprite GameplayChangeThemeBtn;
    public Sprite GameplaySettingsBtn;

}
