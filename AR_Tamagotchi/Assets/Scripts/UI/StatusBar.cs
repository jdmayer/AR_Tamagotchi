using UnityEngine;
using UnityEngine.UI;
using Utils;

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

            var valueText = transform.Find(Constants.Value).GetComponent<Text>();
            valueText.text = value.ToString();
        }

        public void SetMaxValue(int maxValue, int value)
        {
            slider.maxValue = maxValue;
            slider.value = value;

            var valueText = transform.Find(Constants.Value).GetComponent<Text>();
            valueText.text = value.ToString();

            var maxValueText = transform.Find(Constants.MaxValue).GetComponent<Text>();
            maxValueText.text = $"/ {maxValue}";
        }
    }
}
