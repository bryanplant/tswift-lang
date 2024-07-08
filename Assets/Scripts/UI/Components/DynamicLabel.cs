using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace UI.Components
{
    public class DynamicLabel : VisualElement
    {
        private string _text;
        private string _val;

        private Label _textLabel;
        private Label _valLabel;

        public DynamicLabel()
        {
            RegisterCallback<AttachToPanelEvent>(OnAttach);
        }

        private void OnAttach(AttachToPanelEvent evt)
        {
            Children().ToList().ForEach(Remove);
            
            _textLabel = new Label
            {
                text = _text
            };

            _valLabel = new Label
            {
                text = _val
            };

            Add(_textLabel);
            Add(_valLabel);
        }

        public void Setup(ref Action<string> updateValueAction)
        {
            updateValueAction += UpdateValue;
        }

        private void UpdateValue(string val)
        {
            _val = val;
            _valLabel.text = _val;
        }
    }
}