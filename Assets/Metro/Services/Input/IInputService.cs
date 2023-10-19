using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Metro.Services.Input
{
    public interface IInputService : IInitializable, IDisposable
    {
        Vector2 Move { get; }
        UnityAction Tap { get; set; }
    }
}