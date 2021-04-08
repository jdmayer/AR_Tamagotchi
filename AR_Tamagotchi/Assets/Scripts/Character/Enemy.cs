using UI;
using UnityEngine;
using Utils;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Character
{
    public class Enemy
    {
        public int Level;
        public readonly int maxAnger;
        public int Anger;

        private readonly int _maxAttackStrength;
        private readonly int _minAttackStrength;

        private readonly string _name;
        public readonly Dialog StartDialog;
        public readonly Dialog WinDialog;
        public readonly Dialog LoseDialog;

        public Enemy(int playerLevel)
        {
            _name = EnemyDialogs.Names[Random.Range(0, EnemyDialogs.Names.Length)];
            var startSentence = EnemyDialogs.Dialogs[Random.Range(0, EnemyDialogs.Names.Length)];
            StartDialog = new Dialog(_name, startSentence);

            var winSentence = EnemyDialogs.WinDialogs[Random.Range(0, EnemyDialogs.WinDialogs.Length)];
            WinDialog = new Dialog(_name, winSentence);
            
            var loseSentence = EnemyDialogs.LoseDialogs[Random.Range(0, EnemyDialogs.LoseDialogs.Length)];
            LoseDialog = new Dialog(_name, loseSentence);

            Level = Mathf.Max(Random.Range(playerLevel - 2, playerLevel + 2), 1);
            maxAnger = 10 * Level + 100;
            Anger = maxAnger;
            Debug.Log($"Enemy Level: {Level}");
            Debug.Log($"Enemy MaxAnger: {maxAnger}");
        }

        public int DealDamage()
        {
            return Random.Range(_minAttackStrength, _maxAttackStrength);
        }
        
        public void TakeDamage(int damage)
        {
            Anger -= damage;

            if (Anger <= 0)
            {
                Debug.Log("LOL HE DED");
            }
        }
    }
}