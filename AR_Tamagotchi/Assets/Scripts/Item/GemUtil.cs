using Utils;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Item
{
    public static class GemUtil
    {
        public static GemType GetGemTypeByName(string gemName)
        {
            if (Prefabs.EnergyGems.Contains(gemName))
            {
                return GemType.Energy;
            }
            else if (Prefabs.HealthGems.Contains(gemName))
            {
                return GemType.Health;
            }
            else
            {
                return GemType.Experience;
            }
        }
    }
}
