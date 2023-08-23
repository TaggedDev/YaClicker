using System;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    [SerializeField] private float scrollY;
    [SerializeField] private Renderer renderer;
    private float _currentScrollY;

    private void Update()
    {
        renderer.material.mainTextureOffset = new Vector2(0, scrollY * Time.time);
    }
}
