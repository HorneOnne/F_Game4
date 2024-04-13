using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MahjongSO : ScriptableObject
{
    public int ID;
    public Sprite Icon;
    public Sprite GalleryActiveIcon;
    public Sprite GalleryDeactiveIcon;
    public bool Unlock;
}
