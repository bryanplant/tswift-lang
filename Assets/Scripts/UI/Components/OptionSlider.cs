using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine.UIElements;

namespace UI.Components
{
    public class OptionSlider : VisualElement
    {
        private readonly bool _isInt;
        private readonly float _min;
        private readonly float _max;
        private readonly float _startValue;
        private readonly string _label;

        private GenericSlider _slider;

        private const string UssClassName = "option-slider";
        private const string LabelContainerUssClassName = UssClassName + "__label-container";
        private const string LabelUssClassName = UssClassName + "__label";
        private const string ValueContainerUssClassName = UssClassName + "__value-container";
        private const string ValueUssClassName = UssClassName + "__value";
        private const string SliderUssClassName = UssClassName + "__slider";
        private const string InputUssClassName = UssClassName + "__input";
        private const string DragContainerUssClassName = UssClassName + "__drag-container";
        private const string TrackerUssClassName = UssClassName + "__tracker";
        private const string DraggerBorderContainerUssClassName = UssClassName + "__dragger-border";
        private const string DraggerUssClassName = UssClassName + "__dragger";

        public OptionSlider()
        {
            Setup();
        }

        public OptionSlider(bool isInt, string label, float min, float max, float startValue)
        {
            _isInt = isInt;
            _label = label;
            _min = min;
            _max = max;
            _startValue = startValue;

            Setup();
        }
        
        public void RegisterOnFloatChangeCallback(Action<float> callback)
        {
            _slider.RegisterValueChangedCallback((ChangeEvent<float> evt) => callback(evt.newValue));
        }

        public void RegisterOnIntChangeCallback(Action<int> callback)
        {
            _slider.RegisterValueChangedCallback(evt => callback(evt.newValue));
        }

        private void Setup()
        {
            Children().ToList().ForEach(Remove);

            AddToClassList(UssClassName);

            var labelContainer = new VisualElement();
            labelContainer.AddToClassList(LabelContainerUssClassName);

            var label = new Label
            {
                text = _label
            };
            label.AddToClassList(LabelUssClassName);

            var valueContainer = new VisualElement();
            valueContainer.AddToClassList(ValueContainerUssClassName);

            var value = new Label
            {
                text = _startValue.ToString(CultureInfo.InvariantCulture)
            };
            value.AddToClassList(ValueUssClassName);

            _slider = new GenericSlider(_isInt, _min, _max, _startValue);
            _slider.Slider().AddToClassList(SliderUssClassName);

            if (_isInt)
            {
                _slider.RegisterValueChangedCallback((ChangeEvent<int> val) =>
                    value.text = val.newValue.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _slider.RegisterValueChangedCallback((ChangeEvent<float> val) =>
                    value.text = val.newValue.ToString(CultureInfo.InvariantCulture));
            }

            var input = _slider.InputElement();
            input.AddToClassList(InputUssClassName);

            var dragContainer = input.Children().ToList()[0];
            dragContainer.AddToClassList(DragContainerUssClassName);

            var tracker = dragContainer.Query("unity-tracker").First();
            tracker.AddToClassList(TrackerUssClassName);

            var draggerBorder = dragContainer.Query("unity-dragger-border").First();
            draggerBorder.AddToClassList(DraggerBorderContainerUssClassName);

            var dragger = dragContainer.Query("unity-dragger").First();
            dragger.AddToClassList(DraggerUssClassName);

            labelContainer.Add(label);
            Add(labelContainer);
            
            Add(_slider.Slider());
            
            valueContainer.Add(value);
            Add(valueContainer);
        }
    }
}