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

        public Enemy(int playerLevel, StatusBar statusBar, GameObject statusGameObject)
        {
            _name = EnemyDialogs.Names[Random.Range(0, EnemyDialogs.Names.Length)];
            var startSentence = EnemyDialogs.Dialogs[Random.Range(0, EnemyDialogs.Names.Length)];
            StartDialog = new Dialog(_name, startSentence);

            var winSentence = EnemyDialogs.WinDialogs[Random.Range(0, EnemyDialogs.WinDialogs.Length)];
            WinDialog = new Dialog(_name, winSentence, EnemyDialogs.PlayerLoseInformation);
            
            var loseSentence = EnemyDialogs.LoseDialogs[Random.Range(0, EnemyDialogs.LoseDialogs.Length)];
            LoseDialog = new Dialog(_name, loseSentence, EnemyDialogs.PlayerWinInformation1, EnemyDialogs.PlayerWinInformation2);

            var laughSentence = EnemyDialogs.Laughs[Random.Range(0, EnemyDialogs.Laughs.Length)];
            LaughDialog = new Dialog(_name, laughSentence);

            Level = Mathf.Max(Random.Range(playerLevel - 2, playerLevel + 2), 1);
            _maxAnger = 10 * Level + 100;
            Anger = _maxAnger;

            _minAttackStrength = Level * 10;
            _maxAttackStrength = _maxAnger / 2;

            StatusBar = statusBar;
            StatusBar.SetMaxValue(_maxAnger, _maxAnger);

            var statusLabel = statusGameObject.gameObject.transform.Find("Text").GetComponent<Text>();
            statusLabel.text = $"Level {Level}";
        }

        public int DealDamage()
        {
            Debug.Log(_minAttackStrength);
            Debug.Log(_maxAttackStrength);
            return Random.Range(_minAttackStrength, _maxAttackStrength);
        }
        
        public void TakeDamage()
        {
            var maxDamage = (int)(_maxAnger * 0.75);
            Anger -= Random.Range(10, maxDamage);
            Anger = Mathf.Max(0, Anger);
            StatusBar.SetValue(Anger);
        }
    }
}