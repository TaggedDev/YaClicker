using UnityEngine;

namespace UI
{
    /// <summary>
    /// Canvas opening/closing system
    /// </summary>
    public abstract class MyCanvas : MonoBehaviour 
    {
        public abstract CanvasLayer CanvasLayer { get; }
    }
}