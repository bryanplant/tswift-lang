using UI.Common;
using UnityEngine.UIElements;

namespace UI.Game.Panels
{
    public class StartPanel : UIPanel
    {
        public override void Load(UIDocument document)
        {
            var container = new VisualElement();
            document.rootVisualElement.Add(container);
            container.AddToClassList("start-container");
            // ignore click events on the container
            container.pickingMode = PickingMode.Ignore;
            
            panel = new GroupBox();
            container.Add(panel);
            panel.AddToClassList("start-panel");
            
            var startButton = new Button
            {
                text = "Start Game"
            };
            startButton.AddToClassList("button");
            startButton.clicked += Events.StartButtonPressed;
            panel.Add(startButton);
        }
    }
}