using System;
using MiniMvvm;
using MiniViewer3D.Models;

namespace MiniViewer3D.ViewModels
{
    public abstract class TabViewModel : PropertyChangedBase, ISceneStateHandle
    {
        public abstract string Name { get; }

        public abstract event RequestSceneUpdateHandler RequestSceneUpdate;

        public abstract void Initialize(SceneAttribute scene);

        public abstract void Cleanup();
    }
}