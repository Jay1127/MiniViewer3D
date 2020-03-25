using System;
using System.Collections.Generic;
using System.Linq;
using MiniEyes.WpfHelperTools;
using MiniMvvm;
using MiniViewer3D.Models;

namespace MiniViewer3D.ViewModels
{
    public class TabLayoutViewModel : PropertyChangedBase, ISceneStateHandle
    {
        private List<TabViewModel> _tabViewModels;

        public event RequestSceneUpdateHandler RequestSceneUpdate;

        public SceneTabViewModel SceneTabViewModel { get; }

        public NodeTreeTabViewModel NodeTreeTabViewModel { get; }

        public MaterialTabViewModel MaterialTabViewModel { get; }

        public LightsTabViewModel LightsTabViewModel { get; }

        public TabLayoutViewModel(IDialogService dialogService)
        {
            SceneTabViewModel = new SceneTabViewModel(dialogService);
            NodeTreeTabViewModel = new NodeTreeTabViewModel(dialogService);
            MaterialTabViewModel = new MaterialTabViewModel();
            LightsTabViewModel = new LightsTabViewModel(dialogService);

            _tabViewModels = new List<TabViewModel>();
            _tabViewModels.Add(SceneTabViewModel);
            _tabViewModels.Add(NodeTreeTabViewModel);
            _tabViewModels.Add(MaterialTabViewModel);
            _tabViewModels.Add(LightsTabViewModel);
        }

        public void Cleanup()
        {
            _tabViewModels.ForEach(tab =>
            {
                tab.Cleanup();
                tab.RequestSceneUpdate -= OnRequestSceneUpdate;
            });
        }

        public void Initialize(SceneAttribute sceneNode)
        {
            _tabViewModels.ForEach(tab =>
            {
                tab.Initialize(sceneNode);
                tab.RequestSceneUpdate += OnRequestSceneUpdate;
            });
        }

        private void OnRequestSceneUpdate()
        {
            SceneTabViewModel.SceneNode.Scene.ReDraw();
        }
    }
}