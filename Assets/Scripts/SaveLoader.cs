using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class SaveLoader : MonoBehaviour
{
    [Tooltip("First canvas must be player HUD")] [SerializeField] private List<ClickerCanvas> canvases;

    private void Start()
    {
        HandleCanvasLoading();
    }

    private void HandleCanvasLoading()
    {
        CanvasLayersController.Canvases.AddRange(canvases);
        CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.MainMenu);
    }
}