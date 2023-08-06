using UnityEngine;

namespace UI
{
    /// <summary>
    /// Canvas opening/closing system
    /// </summary>
    public abstract class ClickerCanvas : MonoBehaviour 
    {
        public abstract CanvasLayer CanvasLayerTag { get; }
    }
}