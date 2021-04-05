using System.Collections.Generic;
/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Utils
{
    public static class Constants
    {
        //Scenes
        public const string SceneMain = "AR_Main_FinoMarker";
        public const string SceneAdventure = "AR_Main_FinoMarker_Extended";

        //Key Input
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
        public const string Jump = "Jump";

        //CoRoutines
        public const string CharacterMove = "CharacterMove";

        //Animations
        public const string Loop = "Action";
        public const string PlayIdle = "PlayIdle";
        public const string Talk = "Talk";
        public const string Laugh = "Laugh";
        public const string Attack = "Attack";
        public const string Die = "Die";
        public const string GetHit = "GetHit";
        public const string Dizzy = "Dizzy";

        //Prefabs
        public const string ExclamationMark = "ExclamationMark";
        public const string QuestionMark = "QuestionMark";
        public const string Vegetation = "Vegetation";

        public const string Bush_1_1 = "Bush_1_1";
        public const string Bush_1_2 = "Bush_1_2";
        public const string Grass_2_1 = "Grass_2_1";
        public const string Plant_1_1 = "Plant_1_1";
        public const string FallenTree_1_1 = "FallenTree_1_1";
        public const string VegetationDirectory = "Models/FreeVegetation-LowPolyNature/FreeVegetation/Prefabs/";

        public static readonly string[] VegetationPrefabs =
            new string[]
            {
                Bush_1_1,
                Bush_1_2,
                Grass_2_1,
                FallenTree_1_1
            };
    }
}
