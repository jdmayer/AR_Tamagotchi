using UnityEngine;
using Utils;
using Vuforia;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Interaction
{
    public class AdventureAction : MonoBehaviour, IVirtualButtonEventHandler
    {
        public RandomAdventure RandomAdventure;

        void Start()
        {
            GameObject vb = GameObject.Find("VirtualButton");
            vb.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        }
                
        public void OnButtonPressed(VirtualButtonBehaviour vb)
        {
            if (RandomAdventure.State == AdventureState.IsTickling)
            {
                RandomAdventure.EnemyReaction();
            }
            else if (RandomAdventure.State == AdventureState.HasGem)
            {
                RandomAdventure.UseGemStone();
            }
        }

        public void OnButtonReleased(VirtualButtonBehaviour vb)
        {
            if (RandomAdventure.State == AdventureState.HasGem)
            {
                RandomAdventure.UsedGemStone();
            }
        }
    }
}
