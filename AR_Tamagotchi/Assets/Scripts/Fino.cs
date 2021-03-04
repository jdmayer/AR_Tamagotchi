using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Janine Mayer
/// </summary>
public class Fino : MonoBehaviour
{
    public string Name;
    public int Age;
    public int Happiness;
    public int Hunger;
    public int Energy;
    public int Socializing;

    public Fino(string name, int age, int happiness, int hunger, int energy, int socializing)
    {
        Name = name;
        Age = age;
        Happiness = happiness;
        Hunger = hunger;
        Energy = energy;
        Socializing = socializing;
    }

    public Fino()
    {
        Name = "Fino";
        Age = 0;
        Happiness = 100;
        Hunger = 100;
        Energy = 100;
        Socializing = 100;
    }

    private float NextAging = 0.0f;
    private float NextStatChange = 0.0f;
    public float AgePeriod = 300.0f; //5 minute
    public float StatPeriod = 6.0f;

    public Text statsText;

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time > NextAging)
        {
            NextAging += AgePeriod;
            Age++;
        }

        if (Time.time > NextStatChange)
        {
            NextStatChange += StatPeriod;
            Happiness--;
            Hunger--;
            Energy--;
            Socializing--;

            statsText.text = $"Age: {Age} \n\n" +
                $"Happy: {Happiness} \nHunger: {Hunger}\nEnergy: {Energy}\nSocializing: {Socializing}";
        }

        if (Hunger <= 0 || Energy <= 0 || Happiness <= 0 && Socializing <= 0)
        {
            statsText.text += "... DEAD ...";
            Debug.Log($"You killed {Name}.");
        }
    }
}
