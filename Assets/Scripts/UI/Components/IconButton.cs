using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using Button = UnityEngine.UIElements.Button;

namespace UI.Components
{
    public class IconButton : Button
    {
        private const string UssClassName = "icon-button";
        private const string IconContainerUssClassName = UssClassName + "__icon-container";
        private const string IconUssClassName = UssClassName + "__icon";
        private const string LabelContainerUssClassName = UssClassName + "__label-container";
        private const string LabelUssClassName = UssClassName + "__label";
        private const string NumberContainerUssClassName = UssClassName + "__number-container";
        private const string NumberUssClassName = UssClassName + "__number";
        
        private readonly Label _numberLabel;

        public IconButton(string name, Texture icon)
        {
            Children().ToList().ForEach(Remove);
            
            AddToClassList(UssClassName);
            
            var iconContainer = new VisualElement();
            iconContainer.AddToClassList(IconContainerUssClassName);
            Add(iconContainer);
            var iconElement = new Image
            {
                image = icon
            };
            iconElement.AddToClassList(IconUssClassName);
            iconContainer.Add(iconElement);
            
            var labelContainer = new VisualElement();
            labelContainer.AddToClassList(LabelContainerUssClassName);
            Add(labelContainer);
            var label = new Label
            {
                text = "Spawn " + name
            };
            label.AddToClassList(LabelUssClassName);
            labelContainer.Add(label);
            
            var numberContainer = new VisualElement();
            numberContainer.AddToClassList(NumberContainerUssClassName);
            Add(numberContainer);
            _numberLabel = new Label
            {
                text = "0"
            };
            _numberLabel.AddToClassList(NumberUssClassName);
            numberContainer.Add(_numberLabel);
        }

        public void RegisterClickCallback(Action callback)
        {
            clicked += callback;
        }
        
        public void OnNumberChanged(int number)
        {
            _numberLabel.text = number.ToString();
        }
    }
}