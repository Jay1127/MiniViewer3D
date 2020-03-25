using System;
using MiniViewer3D.Models;

namespace MiniViewer3D.ViewModels
{
    /// <summary>
    /// Scene을 Update하도록 요청함.
    /// </summary>
    public delegate void RequestSceneUpdateHandler();

    /// <summary>
    /// Scene을 기반으로 상태 핸들링
    /// </summary>
    public interface ISceneStateHandle
    {
        event RequestSceneUpdateHandler RequestSceneUpdate;
        void Initialize(SceneAttribute sceneNode);
        void Cleanup();
    }
}
