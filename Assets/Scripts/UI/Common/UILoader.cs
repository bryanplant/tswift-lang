using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Common
{
    [ExecuteAlways]
    public class UILoader : MonoBehaviour
    {
        [SerializeField] private string ussClassName = "document";
        [SerializeField] private GameObject panelContainer;
        
        public void OnEnable()
        {
            Refresh();
        }

        public void Refresh()
        {
            var document = GetComponent<UIDocument>(); 
            if (document == null || document.rootVisualElement == null)
            {
                return;
            }

            Debug.Log("Refresh panels");

            document.rootVisualElement.Clear();
            document.rootVisualElement.styleSheets.Clear();
            document.rootVisualElement.AddToClassList(ussClassName);
            var panels = panelContainer.GetComponents<UIPanel>();
            foreach (var panel in panels)
            {
                panel.Load(document);
            }
            var styleSheet = Resources.Load("Styles/Compiled/style", typeof(StyleSheet)) as StyleSheet;
            if (styleSheet != null)
            {
                document.rootVisualElement.styleSheets.Add(styleSheet);
            }
        }
    }
}