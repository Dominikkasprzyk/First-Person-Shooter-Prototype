using UnityEngine;
using UnityEngine.UI;

public class CrosshairChanger : MonoBehaviour
{
    [SerializeField] private Image crosshair;
    public void ChangeCrosshair(Sprite _crosshairSprite)
    {
        crosshair.sprite = _crosshairSprite;
    }
}
