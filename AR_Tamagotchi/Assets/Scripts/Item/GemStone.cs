using UI;
using UnityEngine;

namespace Item
{
    public class GemStone : MonoBehaviour
    {
        public GameObject Gem;

        private GemType _gemType;
        private int _power;

        public GemStone(GemType type)
        {
            _gemType = type;
            _power = Random.Range(1, 4) * 10;
        }

        public void UseGemStone()
        {
            _power = 0;
            Destroy(Gem);
        }

        public Dialog GetGemDialog()
        {
            switch(_gemType)
            {
                case GemType.Health:
                    return new Dialog("Health Stone", $"This stone gives you {_power} Health Points.");
                case GemType.Experience:
                    return new Dialog("Experience Stone", $"This stone gives you {_power} Experience Points.");
                case GemType.Energy:
                default:
                    return new Dialog("Energy Stone", $"This stone gives you {_power} Energy Points.");
            }
        }
    }
}