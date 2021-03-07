using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tamagotchi : MonoBehaviour
{
    public Image currentHappy;
    public Image currentEnergy;
    public Image currentHunger;
    public Image currentSocial;
    
    public Text HappyText;
    public Text EnergyText;
    public Text HungerText;
    public Text SocialText;

    private float happiness = 100;
    private float energy = 100;
    private float hunger = 100;
    private float social = 100;
    private float max = 100;

    public Button Feed;
    public Button Energy;
    public Button Play;
    public Button Social;

    public Text GameOverText;

    private void Start()
    {
    //button Listener for Hunger
    Button btn1 = Feed.GetComponent<Button>();
        btn1.onClick.AddListener(FeedThePet);

    //button Listener for Happiness
    Button btn2 = Play.GetComponent<Button>();
        btn2.onClick.AddListener(PlayWithPet);

    //button Listener for Energy
    Button btn3 = Energy.GetComponent<Button>();
        btn3.onClick.AddListener(EnergyThePet);

    //button Listener for Social
    Button btn4 = Social.GetComponent<Button>();
        btn4.onClick.AddListener(SocialThePet);

        UpdateHungerBar();
        UpdateHappyBar();
        UpdateEnergyBar();
        UpdateSocialBar();

        Update();
    }

   
    private void Update()
    {
        //This deplishes happiness over time
        happiness -= 5.5f * Time.deltaTime;
        if(happiness < 0)
        {
            happiness = 0;
        }
        //This deplishes hunger over time
        hunger -= 6f * Time.deltaTime;
        if(hunger < 0)
        {
            hunger = 0;
        }
        //This deplishes energy over time
        energy -= 5.75f * Time.deltaTime;
        if(energy < 0)
        {
            energy = 0;
        }
        //This deplishes social over time
        social -= 5.75f * Time.deltaTime;
        if(social < 0)
        {
            social = 0;
        }

        UpdateHappyBar();
        UpdateHungerBar();
        UpdateEnergyBar();
        UpdateSocialBar();

        GameOver();
       
    }
    private void UpdateHappyBar() 
    {
        float ratio = happiness/max;
        currentHappy.rectTransform.localScale = new Vector3(ratio, 1, 1);
        HappyText.text = (ratio*100).ToString("0") + '%';
    }
    private void UpdateEnergyBar() 
    {
        float ratio = energy/max;
        currentEnergy.rectTransform.localScale = new Vector3(ratio, 1, 1);
        EnergyText.text = (ratio*100).ToString("0") + '%';
    }
    private void UpdateHungerBar() 
    {
        float ratio = hunger/max;
        currentHunger.rectTransform.localScale = new Vector3(ratio, 1, 1);
        HungerText.text = (ratio*100).ToString("0") + '%';
       
    }
    private void UpdateSocialBar() 
    {
        float ratio = social/max;
        currentSocial.rectTransform.localScale = new Vector3(ratio, 1, 1);
        SocialText.text = (ratio*100).ToString("0") + '%';
       
    }
    void FeedThePet()
    {
        Debug.Log("Feed Button has been clicked");
        hunger += 10;
        if (hunger > max)
        {
            hunger = max;
        }
        UpdateHungerBar();
    }
    void PlayWithPet()
    {
        Debug.Log("Play Button has been clicked");
        happiness += 20;
        if (happiness > max)
        {
            happiness = max;
        }
        UpdateHappyBar();
    }
    void EnergyThePet()
    {
        Debug.Log("Energy Button has been clicked");
        energy += (max-energy);
        if (energy > max)
        {
            energy = max;
        }
        UpdateEnergyBar();
    }
    void SocialThePet()
    {
        Debug.Log("Social Button has been clicked");
        social += (max-social);
        if (social > max)
        {
            social = max;
        }
        UpdateSocialBar();
    }
    void GameOver()
    {
        if (happiness == 0)
        {
            if (energy == 0)
            {
                if (hunger == 0)
                {
                    if (social == 0)
                    {
                           //Feed.gameObject.SetActive(false);
                           //Energy.gameObject.SetActive(false);
                           //Social.gameObject.SetActive(false);
                           //Play.gameObject.SetActive(false);
                           GameOverText.gameObject.SetActive(true);
                    }
                }
            }
        }
    }



}
