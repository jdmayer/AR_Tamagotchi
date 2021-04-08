using UI;
using UnityEngine;
using UnityEngine.UI;
using Utils;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Character
{
    public class Enemy
    {
        public int Level;
        public int Anger;
        private readonly int _maxAnger;

        private readonly int _maxAttackStrength;
        private readonly int _minAttackStrength;

        private readonly string _name;
        public readonly Dialog StartDialog;
        public readonly Dialog WinDialog;
        public readonly Dialog LoseDialog;
        public readonly Dialog LaughDialog;

        public StatusBar StatusBar;

        public Enemy(int playerLevel, StatusBar statusBar)
        {
            _name = EnemyDialogs.Names[Random.Range(0, EnemyDialogs.Names.Length)];
            var startSentence = EnemyDialogs.Dialogs[Random.Range(0, EnemyDialogs.Names.Length)];
            StartDialog = new Dialog(_name, startSentence);

            var winSentence = EnemyDialogs.WinDialogs[Random.Range(0, EnemyDialogs.WinDialogs.Length)];
            WinDialog = new Dialog(_name, winSentence);
            
            var loseSentence = EnemyDialogs.LoseDialogs[Random.Range(0, EnemyDialogs.LoseDialogs.Length)];
            LoseDialog = new Dialog(_name, loseSentence);

            var laughSentence = EnemyDialogs.Laughs[Random.Range(0, EnemyDialogs.Laughs.Length)];
            LaughDialog = new Dialog(_name, laughSentence);

            Level = Mathf.Max(Random.Range(playerLevel - 2, playerLevel + 2), 1);
            _maxAnger = 10 * Level + 100;
            Anger = _maxAnger;
            Debug.Log($"Enemy Level: {Level}");

            StatusBar = statusBar;
            StatusBar.SetMaxValue(_maxAnger, _maxAnger);
            //TODO set level and text!
        }

        public int DealDamage()
        {
            return Random.Range(_minAttackStrength, _maxAttackStrength);
        }
        
        public void TakeDamage()
        {
            var maxDamage = (int)(_maxAnger * 0.75);
            Anger -= Random.Range(10, maxDamage);
            Anger = Mathf.Max(0, Anger);
            StatusBar.SetValue(Anger);

            if (Anger <= 0)
            {
                Debug.Log("LOL HE DED");
            }
        }

        //TODO destroy gameobject when win/lose after final dialog

        public void UpdateStatPosition(Transform parent)
        {
            StatusBar.gameObject.transform.position = Camera.main.WorldToScreenPoint(parent.position); 
        }
    }
}