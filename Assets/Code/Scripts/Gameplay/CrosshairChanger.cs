using UnityEngine;
using UnityEngine.UI;

public class CrosshairChanger : MonoBehaviour
{
    [SerializeField] private Image crosshair;
    [SerializeField] private Sprite fireCrosshair;
    [SerializeField] private Sprite waterCrosshair;
    [SerializeField] private Sprite plantCrosshair;

    public void ChangeCrosshair(Component sender, object data)
    {
        switch ((Fabric)data)
        {
            case Fabric.Fire:
                crosshair.sprite = fireCrosshair;
                break;
            case Fabric.Water:
                crosshair.sprite = waterCrosshair;
                break;
            case Fabric.Plant:
                crosshair.sprite = plantCrosshair;
                break;
        }

    }
}
