using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlipbookAnimator : MonoBehaviour
{
    [SerializeField] private bool animateOnStart;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private List<Texture2D> frames;
    [SerializeField] private Texture2D stopFrame;
    [SerializeField] private float interval;
    private Sequence _sequence;
    private int _frameIndex;

    private void Start()
    {
        if (animateOnStart)
            Animate();
    }

    public void Animate()
    {
        if (frames.Count == 0)
            return;
        
        _sequence = DOTween.Sequence();
        _sequence
            .AppendInterval(interval)
            .SetLoops(-1)
            .AppendCallback(IterateFrames);
    }

    public void StopAnimation()
    {
        _sequence.Kill();
        
        if (stopFrame)
            SetTexture(stopFrame);
    }
    
    private void IterateFrames()
    {
        if (_frameIndex < frames.Count - 1)
            _frameIndex++;
        else
            _frameIndex = 0;
        
        SetTexture(frames[_frameIndex]);
    }

    private void SetTexture(Texture2D _texture)
    {
        if (meshRenderer)
            meshRenderer.material.SetTexture("_BaseMap", _texture);
    }
}