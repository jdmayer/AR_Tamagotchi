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
        public readonly int maxHealth;
        public int Health;

        private readonly int _maxAttackStrength;
        private readonly int _minAttackStrength;

        private readonly string _name;
        private readonly Dialog _startDialog;
        private readonly Dialog _winDialog;
        private readonly Dialog _loseDialog;

        public Enemy(int playerLevel)
        {
            _name = EnemyDialogs.Names[Random.Range(0, EnemyDialogs.Names.Length)];
            var startSentence = EnemyDialogs.Dialogs[Random.Range(0, EnemyDialogs.Names.Length)];
            _startDialog = new Dialog(_name, startSentence);

            var winSentence = EnemyDialogs.WinDialogs[Random.Range(0, EnemyDialogs.Names.Length)];
            _winDialog = new Dialog(_name, winSentence);
            
            var loseSentence = EnemyDialogs.LoseDialogs[Random.Range(0, EnemyDialogs.Names.Length)];
            _loseDialog = new Dialog(_name, loseSentence);

            Level = Random.Range(playerLevel - 2, playerLevel + 2);
            maxHealth = 10 * Level + 100;
            Health = maxHealth;
            Debug.Log($"Enemy Level: {Level}");
            Debug.Log($"Enemy MaxHealth: {maxHealth}");
        }

        public int DealDamage()
        {
            return Random.Range(_minAttackStrength, _maxAttackStrength);
        }
        
        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Debug.Log("LOL HE DED");
            }
        }
    }
}