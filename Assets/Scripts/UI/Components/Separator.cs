using UnityEngine.UIElements;

namespace UI.Components
{
    public class Separator : VisualElement
    {
        private const string UssClassName = "separator";

        public Separator()
        {
            AddToClassList(UssClassName);
        }
    }
}