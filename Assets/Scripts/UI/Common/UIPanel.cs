using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Common
{
    public abstract class UIPanel : MonoBehaviour
    {
        protected VisualElement panel;
        
        public abstract void Load(UIDocument document);
        
        public void Show()
        {
            panel.style.display = DisplayStyle.Flex;
        }
        
        public void Hide()
        {
            panel.style.display = DisplayStyle.None;
        }
    }
}