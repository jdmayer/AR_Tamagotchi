/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Utils
{
    public static class Prefabs
    {
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

        public const string DragonNeutral = "micro_dragon_fino";
        public const string DragonHappy = "micro_dragon_fino_happy";
        public const string DragonSleeping = "micro_dragon_fino_sleeping";
        public const string DragonDead = "micro_dragon_fino_dead";
        public const string DragonDirectory = "Models/Micro-Dragon-Fino/Prefabs/";

        public static readonly string[] DragonPrefabs =
            new string[]
            {
                DragonNeutral,
                DragonHappy
            };
    }
}
