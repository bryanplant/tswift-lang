using System;
using UI.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Game.Panels
{
    public class FpsPanel : UIPanel
    {
        [SerializeField] private float fpsUpdateInterval = 0.5f;
        
        private Action<float> _updateFpsAction;
        private int _numFrames;
        private float _currentFpsUpdateInterval;
        
        public override void Load(UIDocument document)
        {
            var container = new VisualElement();
            document.rootVisualElement.Add(container);
            container.AddToClassList("fps-container");
            
            panel = new GroupBox();
            container.Add(panel);
            panel.AddToClassList("fps-panel");

            var label = new Label
            {
                text = "0 FPS"
            };
            panel.Add(label);
            
            _updateFpsAction += val => label.text = Math.Floor(val) + " FPS";
        }
        
        private void Update()
        {
            UpdateFps();
        }
        
        private void UpdateFps()
        {
            _currentFpsUpdateInterval += Time.deltaTime;
            _numFrames += 1;
            if (_currentFpsUpdateInterval < fpsUpdateInterval)
            {
                return;
            }

            var fps = _numFrames / _currentFpsUpdateInterval;
            _currentFpsUpdateInterval = 0f;
            _numFrames = 0;
            _updateFpsAction?.Invoke(fps);
        }
    }
}