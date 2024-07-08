using UI.Common;
using UI.Game.Panels;
using UnityEngine;

namespace UI.Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject panelContainer;
        
        private UIPanel _fpsPanel;
        private UIPanel _startPanel;
        
        private void Start()
        {
            _fpsPanel = panelContainer.GetComponentInChildren<FpsPanel>();
            _startPanel = panelContainer.GetComponentInChildren<StartPanel>();
            _startPanel.Show();
            _fpsPanel.Hide();
            
            Events.RegisterStartButtonPressed(OnStartButtonPressed);
        }
        
        private void OnStartButtonPressed()
        {
            _startPanel.Hide();
            _fpsPanel.Show();
        }
    }
}