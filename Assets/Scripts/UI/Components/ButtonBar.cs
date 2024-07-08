using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
using UI.Common;
using UnityEngine;

namespace UI.Components
{
    public class ButtonBar : VisualElement
    {
        private int _buttonCount;

        private List<Button> _buttons;
        private int _currentButton;
        private Action<string> _onChangeCallback;
        
        private const string UssClassName = "button-bar";
        private const string ButtonUssClassName = UssClassName + "__button";
        private const string ButtonSelectedUssClassName = UssClassName + "__button-selected";

        public void Setup(List<string> buttonNames, int val, Action<string> callback)
        {
            AddToClassList(UssClassName);
            
            SetupButtons(buttonNames);
            UpdateValue(val);
            _onChangeCallback = callback; 
        }

        private void SetupButtons(IReadOnlyList<string> buttonText)
        {
            Children().ToList().ForEach(Remove);
            _buttons = new List<Button>();
            for (var i = 0; i < buttonText.Count; i++)
            {
                var index = i;
                var button = new Button(() => UpdateValue(index))
                {
                    text = buttonText[i]
                };
                button.AddToClassList(ButtonUssClassName);
                Add(button);
                _buttons.Add(button);
            }
            
            _buttons.ForEach(Add);
        }

        private void UpdateValue(int val)
        {
            Util.SwapClass(_buttons[_currentButton], ButtonSelectedUssClassName, ButtonUssClassName);
            Util.SwapClass(_buttons[val], ButtonUssClassName, ButtonSelectedUssClassName);
            _currentButton = val;

            _onChangeCallback?.Invoke(_buttons[val].text);
        }
    }
}