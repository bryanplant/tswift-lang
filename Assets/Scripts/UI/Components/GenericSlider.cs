using System.Linq;
using UnityEngine.UIElements;

namespace UI.Components
{ 
    public class GenericSlider : VisualElement
    {
        private readonly bool _isInt;

        private readonly Slider _slider;
        private readonly SliderInt _sliderInt;

        public GenericSlider(bool isInt, float min, float max, float startValue)
        {
            _isInt = isInt;

            if (_isInt)
            {
                _sliderInt = new SliderInt((int) min, (int) max)
                {
                    value = (int) startValue
                };

                Add(_sliderInt);
            }
            else
            {
                _slider = new Slider(min, max)
                {
                    value = startValue
                };

                Add(_slider);
            }
        }

        public void RegisterValueChangedCallback(EventCallback<ChangeEvent<float>> func)
        {
            _slider.RegisterValueChangedCallback(func);
        }


        public void RegisterValueChangedCallback(EventCallback<ChangeEvent<int>> func)
        {
            _sliderInt.RegisterValueChangedCallback(func);
        }

        public VisualElement Slider()
        {
            return _isInt ? _sliderInt : _slider;
        }

        public VisualElement InputElement()
        {
            return _isInt ? _sliderInt.Children().ToList()[0] : _slider.Children().ToList()[0];
        }
    }
}