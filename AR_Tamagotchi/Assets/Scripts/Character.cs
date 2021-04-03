using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Monobehaviours
{
    public class Character : MonoBehaviour
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
            Debug.Log("lvl: " + Level);

            Level = Mathf.RoundToInt(100 * Mathf.Sqrt(ExperiencePoints));
            Debug.Log("XP: " + ExperiencePoints);
            Debug.Log("lvl: " + Level);
        }

        private void SetMaxHealth()
        {
            Debug.Log("mx hlt: " + MaxHealth);

            MaxHealth = 10 * Level + 100;
            Debug.Log("mx hlt: " + MaxHealth);

        }

        private float NextHealing = 0.0f;
        public float HealingPeriod = 10.0f;

        public Text statsText;

        private void Start()
        {
            
        }

        void Update()
        {
            if (Time.time > NextHealing)
            {
                NextHealing += HealingPeriod;
                Health = Health < MaxHealth ? 2 : 0;
            }

            //statsText.text = $"Health: {Health}|{MaxHealth} - XP {ExperiencePoints}";

            //if (Health <= 0)
            //{
            //    statsText.text += "... DEAD ...";
            //    Debug.Log($"You killed FINO.");
            //}
        }
    }
}