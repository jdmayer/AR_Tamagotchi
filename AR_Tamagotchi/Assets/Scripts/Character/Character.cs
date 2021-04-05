using System;
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
        private int _experiencePoints;
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
                ExperienceBar.SetMaxValue(100, 0);
            }
        }

        public int Level;

        private int _energy;
        public int Energy
        {
            get
            {
                return _energy;
            }
            set
            {
                _energy = value;
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
                _health = value;
                HealthBar.SetValue(_health);
            }
        }
        protected int _maxHealth { get; private set; }

        public StatusBar HealthBar;
        public StatusBar EnergyBar;
        public StatusBar ExperienceBar;

        private float NextHealing = 0.0f;
        public float HealingPeriod = 10.0f;

        public Text statsText;

        public Character()
        {
            ExperienceBar.SetMaxValue(100, 100);
            HealthBar.SetMaxValue(100, 100);
            EnergyBar.SetMaxValue(100, 100);

            //TODO - check what else is needed!
            ExperiencePoints = 0;
            _health = 100;
        }

        /// <summary>
        /// Calculation of Level is linear depending on experience points
        /// https://gamedev.stackexchange.com/questions/13638/algorithm-for-dynamically-calculating-a-level-based-on-experience-points
        /// </summary>
        private void SetLevel()
        {
            var newLevel = Mathf.RoundToInt(100 * Mathf.Sqrt(ExperiencePoints));
            Level = Math.Max(newLevel, 1);
        }

        //TODO check if they are reset with new XP
        private void SetMaxHealth()
        {
            var prevMaxHealth = _maxHealth;
            _maxHealth = 10 * Level + 100;

            _health += (_maxHealth - prevMaxHealth);
            HealthBar.SetMaxValue(_maxHealth, _health);
        }

        private void Start()
        {
            UpdateValuesWithPlayerPrefs();
        }

        void Update()
        {
            if (Time.time > NextHealing)
            {
                NextHealing += HealingPeriod;
                _health = _health < _maxHealth ? 2 : 0;

                //TODO rm after testing
                Energy -= 10;
                _health -= 10;
            }

            //statsText.text = $"Health: {Health}|{MaxHealth} - XP {ExperiencePoints}";

            //if (Health <= 0)
            //{
            //    statsText.text += "... DEAD ...";
            //    Debug.Log($"You killed FINO.");
            //}
        }

        public override void UpdateValuesWithPlayerPrefs()
        {
            ExperiencePoints = PlayerPrefs.GetInt(PlayerPref.ExperiencePoints, 0);
            Energy = Convert.ToInt32(PlayerPrefs.GetFloat(PlayerPref.Energy, 100f));
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