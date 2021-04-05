using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace UI
{
    public class StatusBar : MonoBehaviour
    {
        public Slider slider;

        public void SetValue(int value)
        {
            slider.value = value;
        }

        public void SetMaxValue(int maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;
        }
    }
}
