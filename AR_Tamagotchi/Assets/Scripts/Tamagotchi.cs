using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Autorin: Helena Wilde
/// </summary>

public class Tamagotchi : MonoBehaviour
{
    public GameObject bananeObject;
    public GameObject burgerObject;
    public GameObject icecreamObject;
    public GameObject foodThatFinoWantsEat;
    
    int banane = 1;
    int burger = 2;
    int icecream = 3;

    public Image currentHappy;
    public Image currentEnergy;
    public Image currentHunger;
    public Image currentSocial;

    //PopUps
    public Image thunderCloud;
    public Image happinessBubble;
    public Image energyBubble;
    public Image hungerBubble;
    public Image socialBubble;
    
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

    public GameObject FinoHappy;
    public GameObject FinoDead;

    private void Start()
    {
        //button Listener for Hunger
        Button btn1 = Feed.GetComponent<Button>();
        //btn1.onClick.AddListener(FeedTheFino);

        //button Listener for Happiness
        Button btn2 = Play.GetComponent<Button>();
        btn2.onClick.AddListener(PlayWithFino);

        //button Listener for Energy
        Button btn3 = Energy.GetComponent<Button>();
        btn3.onClick.AddListener(EnergyTheFino);

        //button Listener for Social
        Button btn4 = Social.GetComponent<Button>();
        btn4.onClick.AddListener(SocialTheFino);

        //make bubbles not visibile upon game start
        happinessBubble.CrossFadeAlpha(0, 0.001f, true);
        energyBubble.CrossFadeAlpha(0, 0.001f, true);
        hungerBubble.CrossFadeAlpha(0, 0.001f, true);
        socialBubble.CrossFadeAlpha(0, 0.001f, true);
        thunderCloud.CrossFadeAlpha(0, 0.001f, true);

        InvokeRepeating(nameof(ChangeFoodStatus), 0.0f, 1f);

        UpdateHungerBar();
        UpdateHappyBar();
        UpdateEnergyBar();
        UpdateSocialBar();

        Update();
    }
       
    private void Update()
    {
        //This deplishes happiness over time
        happiness -= 0.3f * Time.deltaTime;
        if(happiness < 0)
        {
            happiness = 0;
        }
        //This deplishes hunger over time
        hunger -= 0.5f * Time.deltaTime;
        if(hunger < 0)
        {
            hunger = 0;
        }
        //This deplishes energy over time
        energy -= 0.1f * Time.deltaTime;
        if(energy < 0)
        {
            energy = 0;
        }
        //This deplishes social over time
        social -= 0.2f * Time.deltaTime;
        if(social < 0)
        {
            social = 0;
        }

        UpdateHappyBar();
        UpdateHungerBar();
        UpdateEnergyBar();
        UpdateSocialBar();

        GameOver();

        needsCheck();
        goodParentCheck();
    }
    //check needs of fino
    private void needsCheck()
    {
        if(happiness <= 50)
        {
            happinessBubble.CrossFadeAlpha(1, 0.5f, true); 
        }
        else
        {
            happinessBubble.CrossFadeAlpha(0, 0.5f, true);
        }
        if(hunger < 50)
        {
            hungerBubble.CrossFadeAlpha(1, 0.5f, true);
        }
        else
        {
            hungerBubble.CrossFadeAlpha(0, 0.5f, true);
        }
        if(energy <= 50)
        {
            energyBubble.CrossFadeAlpha(1, 0.5f, true);
        }
        else
        {
            energyBubble.CrossFadeAlpha(0, 0.5f, true);
        }
        if(social <= 50)
        {
            socialBubble.CrossFadeAlpha(1, 0.5f, true);
        }
        else
        {
            socialBubble.CrossFadeAlpha(0, 0.5f, true);
        }
    }

    //determines is fino happy or neglected
    private void goodParentCheck()
    {
        if (happiness <= 20 || hunger <= 20 || energy <= 20 || social <= 20)
        {
            FinoHappy.gameObject.SetActive(true);
            thunderCloud.CrossFadeAlpha(1, 0.5f, true);
        }
        else
        {
            FinoHappy.gameObject.SetActive(true);
            thunderCloud.CrossFadeAlpha(0, 0.5f, true);
        }
    }
    private void ChangeFoodStatus()
    {
        int randomZahl = 0;
        randomZahl = Random.Range(0, 2);

        if (banane == randomZahl)
        {
            foodThatFinoWantsEat = bananeObject;
        }
        else if (burger == randomZahl)
        {
            foodThatFinoWantsEat = burgerObject;
        }
        else if (icecream == randomZahl)
        {
            foodThatFinoWantsEat = icecreamObject;
        }
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
    public void FeedTheFino(GameObject food)
    {
       if (food.name.Equals(foodThatFinoWantsEat?.name))
        {
           hunger += 30;
           if (hunger > max)
           {
               hunger = max;
           }
           UpdateHungerBar();
        }
    }
    void PlayWithFino()
    {
        Debug.Log("Play Button has been clicked");
        happiness += 20;
        if (happiness > max)
        {
            happiness = max;
        }
        UpdateHappyBar();
    }
    void EnergyTheFino()
    {
        Debug.Log("Energy Button has been clicked");
        energy += (max-energy);
        if (energy > max)
        {
            energy = max;
        }
        UpdateEnergyBar();
    }
    void SocialTheFino()
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
                           FinoHappy.gameObject.SetActive(false);
                           FinoDead.gameObject.SetActive(true);
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
