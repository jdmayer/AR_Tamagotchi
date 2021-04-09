using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utils;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Character
{
    public class Character : CharacterBasic
    {
        public string Name;

        private int _experiencePoints;
        private int _maxExperiencePoints;
        public int ExperiencePoints {
            get
            {
                return _experiencePoints;
            }
            set
            {
                _experiencePoints = value;
                SetLevel();
                SetMaxHealth();
            }
        }

        public int Level;

        private const int MaxEnergy = 100;
        private int _energy;
        public int Energy
        {
            get
            {
                return _energy;
            }
            set
            {
                _energy = value > MaxEnergy ? MaxEnergy : value;
                EnergyBar.SetValue(_energy);
            }
        }

        private int _health;
        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value > _maxHealth ? _maxHealth : value;
                _health = Math.Max(_health, 0);
                HealthBar.SetValue(_health);
            }
        }

        protected int _maxHealth { get; private set; }

        public StatusBar HealthBar;
        public StatusBar EnergyBar;
        public StatusBar ExperienceBar;
        public Text LevelText;

        private float _timeLeftUntilRecharge = 0.0f;
        public float RechargeTime = 4.0f;

        public bool IsInFight = false;

        public Text statsText;

        public Character()
        {
            _maxHealth = 100;
            _health = 100;
            _experiencePoints = 0;
            Level = 1;
        }

        /// <summary>
        /// Calculation of Level is linear depending on experience points
        /// https://gamedev.stackexchange.com/questions/13638/algorithm-for-dynamically-calculating-a-level-based-on-experience-points
        /// </summary>
        private void SetLevel()
        {
            const float levelRounder = 0.3f;
            var newLevel = Mathf.RoundToInt(levelRounder * Mathf.Sqrt(ExperiencePoints));
            Level = Math.Max(newLevel, 1);
            LevelText.text = Level.ToString();

            _maxExperiencePoints = Convert.ToInt32(Mathf.Pow(((newLevel + 1) / levelRounder), 2.0f));
            ExperienceBar.SetMaxValue(_maxExperiencePoints, ExperiencePoints);
        }

        private void SetMaxHealth()
        {
            var prevMaxHealth = _maxHealth;
            _maxHealth = 10 * Level + 100;
            Health += (_maxHealth - prevMaxHealth);
            HealthBar.SetMaxValue(_maxHealth, Health);
        }

        private void Start()
        {
            UpdateValuesWithPlayerPrefs();
            StartCoroutine(Constants.UpdateStatus);
        }

        void Update()
        {
            
        }

        public void StartAdventure()
        {
            StopCoroutine(Constants.UpdateStatus);
        }

        public void StopAdventure()
        {
            StartCoroutine(Constants.UpdateStatus);
        }

        IEnumerator UpdateStatus()
        {
            while (true)
            {
                _timeLeftUntilRecharge -= Time.deltaTime;

                if (_timeLeftUntilRecharge < 0)
                {
                    Health += Health < _maxHealth ? 2 : 0;
                    Energy -= Energy > 0 ? 2 : 0;
                    _timeLeftUntilRecharge = RechargeTime;
                }

                yield return null;
            }
        }

        public override void UpdateValuesWithPlayerPrefs()
        {
            Name = PlayerPrefs.GetString(PlayerPref.Name);
            ExperiencePoints = PlayerPrefs.GetInt(PlayerPref.ExperiencePoints, 0);
            Energy = Convert.ToInt32(PlayerPrefs.GetFloat(PlayerPref.Energy, 100f));
            EnergyBar.SetMaxValue(MaxEnergy, Energy);
        }

        public override void SetPlayerPrefs()
        {
            PlayerPrefs.SetFloat(PlayerPref.Energy, Energy);
            PlayerPrefs.SetFloat(PlayerPref.ExperiencePoints, _experiencePoints);
        }

        public override void ResetPlayerPrefs()
        {
            PlayerPrefs.SetFloat(PlayerPref.Energy, 100);
            PlayerPrefs.SetFloat(PlayerPref.ExperiencePoints, 0);
        }
    }
}