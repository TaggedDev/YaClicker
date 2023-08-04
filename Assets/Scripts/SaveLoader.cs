using System;
using UI;
using UnityEngine;

public class SaveLoader : MonoBehaviour
{
    [SerializeField] private MyCanvas[] canvases;

    private void Start()
    {
        CanvasLayersController.Canvases.AddRange(canvases);
        CanvasLayersController.OpenedCanvas = canvases[0];

        if (CanvasLayersController.OpenedCanvas.CanvasLayer != CanvasLayer.PlayerHUD)
            throw new ArgumentException("Opened canvas doesn't have PlayerHUD layer. First canvas must be HUD");
    }
}