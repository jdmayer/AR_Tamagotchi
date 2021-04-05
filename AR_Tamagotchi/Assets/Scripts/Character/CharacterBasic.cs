using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// Usage of abstract class as interfaces are not well compatible with unity 
/// </summary>
namespace Character
{
    public abstract class CharacterBasic : MonoBehaviour
    {
        public abstract void SetPlayerPrefs();
        public abstract void ResetPlayerPrefs();
        public abstract void UpdateValuesWithPlayerPrefs();
    }
}
