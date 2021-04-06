using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Item
{
    public class GemStone : MonoBehaviour
    {
        public GameObject Gem;

        public GemType GemType;
        public int Power;

        GemStone(GemType type)
        {
            GemType = type;
            Power = Random.Range(1, 3) * 10;
        }

        public void UseGemStone()
        {
            Power = 0;
            Destroy(Gem);
        }
    }
}