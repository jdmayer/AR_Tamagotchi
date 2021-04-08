using System.Collections.Generic;
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
                Grass_2_1
            };

        public const string DragonNeutral = "micro_dragon_fino";
        public const string DragonHappy = "micro_dragon_fino_happy";
        public const string DragonSleeping = "micro_dragon_fino_sleeping";
        public const string DragonDead = "micro_dragon_fino_dead";
        public const string DragonDirectory = "Models/Micro-Dragon-Fino/Prefabs/";

        public const string EnergyGem1 = "PurpCrystal00";
        public const string EnergyGem2 = "PurpCrystal05";
        public const string EnergyGem3 = "PurpCrystal07";

        public static readonly List<string> EnergyGems =
            new List<string>
            {
                EnergyGem1,
                EnergyGem2,
                EnergyGem3
            };

        public const string HealthGem1 = "BlueCrystal00";
        public const string HealthGem2 = "BlueCrystal09";
        public const string HealthGem3 = "BlueCrystal10";

        public static readonly List<string> HealthGems =
            new List<string>
            {
                HealthGem1,
                HealthGem2,
                HealthGem3
            };

        public const string ExperienceGem = "GemStone04";
        public const string GemDirectory = "Models/Toon Crystals pack/Meshes/";

        public static readonly string[] GemPrefabs =
            new string[]
            {
                EnergyGem1,
                EnergyGem2,
                EnergyGem3,
                ExperienceGem,
                HealthGem1,
                HealthGem2,
                HealthGem3
            };

        public const string StatusBar = "Prefabs/StatusBarObject";
    }
}
