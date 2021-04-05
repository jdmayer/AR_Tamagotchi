using System;
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
        private int experiencePoints;
        public int ExperiencePoints {
            get
            {
                return experiencePoints;
            }
            set
            {
                experiencePoints = value;
                SetLevel();
                SetMaxHealth(); //check if they are set with new xp points
            }
        }

        public int Level { get; private set; }
        public int Energy { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; private set; }


        public Character()
        {
            ExperiencePoints = 0;
            Health = 100;
        }

        /// <summary>
        /// Calculation of Level is linear depending on experience points
        /// https://gamedev.stackexchange.com/questions/13638/algorithm-for-dynamically-calculating-a-level-based-on-experience-points
        /// </summary>
        private void SetLevel()
        {
            //TODO check if it can become level 0!
            Level = Mathf.RoundToInt(100 * Mathf.Sqrt(ExperiencePoints));
        }

        private void SetMaxHealth()
        {
            MaxHealth = 10 * Level + 100;
        }

        private float NextHealing = 0.0f;
        public float HealingPeriod = 10.0f;

        public Text statsText;

        private void Start()
        {
            UpdateValuesWithPlayerPrefs();
        }

        void Update()
        {
            if (Time.time > NextHealing)
            {
                NextHealing += HealingPeriod;
                Health = Health < MaxHealth ? 2 : 0;

                Energy -= 10;
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
            PlayerPrefs.SetFloat(PlayerPref.ExperiencePoints, experiencePoints);
        }

        public override void ResetPlayerPrefs()
        {
            PlayerPrefs.SetFloat(PlayerPref.Energy, 100);
            PlayerPrefs.SetFloat(PlayerPref.ExperiencePoints, 0);
        }
    }
}