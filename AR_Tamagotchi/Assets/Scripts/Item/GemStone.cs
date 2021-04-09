using UI;
using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Item
{
    public class GemStone : MonoBehaviour
    {
        public Dialog InformationDialog;
        public Dialog UsedDialog;

        private GemType _gemType;
        private int _power;

        public GemStone(GemType type)
        {
            _gemType = type;
            _power = Random.Range(1, 4) * 10;
            SetGemDialogs();
        }

        public void UseGemStone(Character.Character player)
        {
            switch (_gemType)
            {
                case GemType.Health:
                    player.Health += _power;
                    break;
                case GemType.Experience:
                    player.ExperiencePoints += _power;
                    break;
                case GemType.Energy:
                default:
                    player.Energy += _power;
                    break;
            }
        }

        public void DestroyGemStone()
        {
            Destroy(gameObject);
        }

        private void SetGemDialogs()
        {
            switch(_gemType)
            {
                case GemType.Health:
                    InformationDialog = new Dialog("Health Stone", $"This stone gives you {_power} Health Points. Grab it!");
                    UsedDialog = new Dialog("Health Stone", $"The health stone was destroyed after usage.");
                    break;
                case GemType.Experience:
                    InformationDialog = new Dialog("Experience Stone", $"This stone gives you {_power} Experience Points. Grab it!");
                    UsedDialog = new Dialog("Experience Stone", $"The experience stone was destroyed after usage.");
                    break;
                case GemType.Energy:
                default:
                    InformationDialog = new Dialog("Energy Stone", $"This stone gives you {_power} Energy Points. Grab it!");
                    UsedDialog = new Dialog("Energy Stone", $"The energy stone was destroyed after usage.");
                    break;
            }
        }
    }
}