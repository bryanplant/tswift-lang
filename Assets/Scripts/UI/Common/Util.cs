using UnityEngine.UIElements;

namespace UI.Common
{
    public static class Util
    {
        public static void SwapClass(VisualElement element, string class1, string class2)
        {
            if (element.ClassListContains(class1))
            {
                element.RemoveFromClassList(class1);
            }

            if (!element.ClassListContains(class2))
            {
                element.AddToClassList(class2);
            }
        }

        public static void SetOnlyClass(VisualElement element, string className)
        {
            element.ClearClassList();
            element.AddToClassList(className);
        }
    }
}