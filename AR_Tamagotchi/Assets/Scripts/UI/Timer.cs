using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace UI
{
    public class Timer : MonoBehaviour
    {
        private Canvas _canvas;
        private Text TimerTextPrefab;
        private Text _countDown;

        private float _rechargeTime;
        private float _timeLeft = 0f;

        public bool HasTimeOut = false;

        public Timer(Canvas canvas, Text timerTextPrefab, float rechargeTime)
        {
            _canvas = canvas;
            TimerTextPrefab = timerTextPrefab;
            _rechargeTime = rechargeTime;
        }

        public void StartTimer(Transform objectTransform)
        {
            if (_countDown == null)
            {
                var countdownPos = Camera.main.WorldToScreenPoint(objectTransform.position);
                _countDown = Instantiate(TimerTextPrefab, _canvas.transform);
                _countDown.transform.position = countdownPos;
            }

            _countDown.enabled = true;
            _timeLeft = _rechargeTime;
            HasTimeOut = true;
        }

        public void StopTimer()
        {
            _countDown.enabled = false;
            HasTimeOut = false;
        }

        public void UpdateTimer(Transform objectTransform)
        {
            _timeLeft -= Time.deltaTime;
            _countDown.text = $"{_timeLeft:00} seconds";
            _countDown.transform.position = Camera.main.WorldToScreenPoint(objectTransform.position);

            if (_timeLeft < 0)
            {
                StopTimer();
            }
        }
    }
}