using UnityEngine;
namespace CodeBase.Service
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsGrabButtonUp { get; }
        bool IsDropButtonUp { get; }
    }

}
