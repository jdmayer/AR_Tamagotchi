using UI;
using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Item
{
    public class GemStone : MonoBehaviour
    {
        private GemType _gemType;
        private int _power;

        public GemStone(GemType type)
        {
            _gemType = type;
            _power = Random.Range(1, 4) * 10;
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

            Debug.Log(_power);
            Debug.Log("Use and destroy!");

            _power = 0;
            Destroy(gameObject);
        }

        public Dialog GetGemDialog()
        {
            switch(_gemType)
            {
                case GemType.Health:
                    return new Dialog("Health Stone", $"This stone gives you {_power} Health Points. Grab it!");
                case GemType.Experience:
                    return new Dialog("Experience Stone", $"This stone gives you {_power} Experience Points. Grab it!");
                case GemType.Energy:
                default:
                    return new Dialog("Energy Stone", $"This stone gives you {_power} Energy Points. Grab it!");
            }
        }
    }
}