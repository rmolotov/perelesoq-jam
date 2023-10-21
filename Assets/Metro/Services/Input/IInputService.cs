using UnityEngine;
using UnityEngine.Events;

namespace Metro.Services.Input
{
    public interface IInputService
    {
        Vector2 Move { get; }
        UnityAction<Vector2> Tap { get; set; }
    }
}