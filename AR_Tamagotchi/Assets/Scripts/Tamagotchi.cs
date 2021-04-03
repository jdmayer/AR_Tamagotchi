using Monobehaviours;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Autorin: Helena Wilde
/// </summary>

public class Tamagotchi : MonoBehaviour
{ 
    public GameObject FinoHappy;
    public GameObject FinoDead;
    public GameObject FinoSleeps;
    public GameObject bananeObject;
    public GameObject burgerObject;
    public GameObject icecreamObject;
    public GameObject teddyBear;
    public GameObject foodThatFinoWantsEat;
    
    int banane = 1;
    int burger = 2;
    int icecream = 3;

    public Image currentHappy;
    public Image currentEnergy;
    public Image currentHunger;
    public Image currentSocial;
    public Image HappyBar;
    public Image HungerBar;
    public Image EnergyBar;
    public Image SocialBar;
    
    public Text HappyText;
    public Text EnergyText;
    public Text HungerText;
    public Text SocialText;
    public Text textAge;

    private float happiness = 100;
    private float energy = 100;
    private float hunger = 100;
    private float social = 100;
    private float max = 100;
    private float startAge;
    private float timer;
    private float sec = 100f * Time.deltaTime;

    private bool gameOver = false;
    private bool wrongFood = false;

    //PopUps
    public Image GameOverImage;
    public Image thunderCloud;
    public Image happinessBubble;
    public Image energyBubble;
    public Image hungerBubble;
    public Image socialBubble;
    public Text FeedMe;
    public Text LetMeSleep;
    public Text HugMe;
    public Text PlayWithMe;
    public Image FoodWishBurger;
    public Image FoodWishBanana;
    public Image FoodWishIceCream;

    [SerializeField] private AnimatorOverrideController overrideController;
    [SerializeField] private AnimatorOverrider overrider;

    private void Start()
    {
        overrider.SetAnimations(overrideController);
        textAge.text = "Age: " + startAge.ToString("F2");

        //make pop ups not visibile upon game start
        happinessBubble.CrossFadeAlpha(0, 0.001f, true);
        energyBubble.CrossFadeAlpha(0, 0.001f, true);
        hungerBubble.CrossFadeAlpha(0, 0.001f, true);
        socialBubble.CrossFadeAlpha(0, 0.001f, true);
        thunderCloud.CrossFadeAlpha(0, 0.001f, true);
        FeedMe.CrossFadeAlpha(0, 0.001f, true);
        LetMeSleep.CrossFadeAlpha(0, 0.001f, true);
        HugMe.CrossFadeAlpha(0, 0.001f, true);
        PlayWithMe.CrossFadeAlpha(0, 0.001f, true);

        InvokeRepeating(nameof(ChangeFoodStatus), 0.0f, 5f);
  
        UpdateHungerBar();
        UpdateHappyBar();
        UpdateEnergyBar();
        UpdateSocialBar();

        Update();
    }

    private void Update()
    {
        if(!gameOver)
        {
            startAge += 0.1f * Time.deltaTime;
            textAge.text = "Age: " + startAge.ToString("F0");
        }

        //This deplishes happiness over time
        happiness -= 1f * Time.deltaTime;
        if(happiness < 0)
        {
            happiness = 0;
        }
        else if(happiness < 10)
        {
            toggleStatusBar(HappyBar);
        }

        //This deplishes hunger over time
        hunger -= 1f * Time.deltaTime;
        if(hunger < 0)
        {
            hunger = 0;
        }
        else if(hunger < 10)
        {
            toggleStatusBar(HungerBar);
        }

        if(!FinoSleeps.gameObject.activeSelf)
        {
            //This deplishes energy over time
            energy -= 1f * Time.deltaTime;
            if(energy < 0)
            {
                energy = 0;
            }
            else if(energy < 10)
            {
                toggleStatusBar(EnergyBar);
            }
        }
        else if(FinoSleeps.gameObject.activeSelf)
        {
            energy += 5f * Time.deltaTime;
            if(energy > 100)
            {
                energy = 100;
                AwakeTheFino();
            }
        }

        //This deplishes social over time
        social -= 1f * Time.deltaTime;
        if(social < 0)
        {
            social = 0;
        }
        else if(social < 10)
        {
            toggleStatusBar(SocialBar);
        }

        UpdateHappyBar();
        UpdateHungerBar();
        UpdateEnergyBar();
        UpdateSocialBar();
        GameOver();

        needsCheck();
        goodParentCheck();
    }

    public void toggleStatusBar(Image image)
    {
        if(image.gameObject.activeSelf)
        {
            image.gameObject.SetActive(false);
        } 
        else
        {
            image.gameObject.SetActive(true);
        }
    }

    //Check needs of fino
    private void needsCheck()
    {
        if(happiness <= 50)
        {
            happinessBubble.CrossFadeAlpha(1, 0.5f, true);
            PlayWithMe.CrossFadeAlpha(1, 0.5f, true); 
        }
        else
        {
            happinessBubble.CrossFadeAlpha(0, 0.5f, true);
            PlayWithMe.CrossFadeAlpha(0, 0.5f, true); 
        }
        
        if(hunger < 50)
        {
            hungerBubble.CrossFadeAlpha(1, 0.5f, true);
            FeedMe.CrossFadeAlpha(1, 0.5f, true); 
        }
        else
        {
            hungerBubble.CrossFadeAlpha(0, 0.5f, true);
            FeedMe.CrossFadeAlpha(0, 0.5f, true); 
        }
        
        if(energy <= 50)
        {
            energyBubble.CrossFadeAlpha(1, 0.5f, true);
            LetMeSleep.CrossFadeAlpha(1, 0.5f, true); 
        }
        else
        {
            energyBubble.CrossFadeAlpha(0, 0.5f, true);
            LetMeSleep.CrossFadeAlpha(0, 0.5f, true); 
        }
        
        if(social <= 50)
        {
            socialBubble.CrossFadeAlpha(1, 0.5f, true);
            HugMe.CrossFadeAlpha(1, 0.5f, true); 
        }
        else
        {
            socialBubble.CrossFadeAlpha(0, 0.5f, true);
            HugMe.CrossFadeAlpha(0, 0.5f, true); 
        }
    }

    //Check needs
    private void goodParentCheck()
    {
        if (happiness <= 20 && hunger <= 20 && energy <= 20 && social <= 20)
        {
            thunderCloud.CrossFadeAlpha(1, 0.5f, true);
        }
        else
        {
            thunderCloud.CrossFadeAlpha(0, 0.5f, true);
        }
    }

    //Change the food wish between burger, banana and icecream
    private void ChangeFoodStatus()
    {
        if (!gameOver)
        {
            FoodWishBanana.gameObject.SetActive(false);
            FoodWishBurger.gameObject.SetActive(false);
            FoodWishIceCream.gameObject.SetActive(false);

            int randomZahl = 0;
            randomZahl = Random.Range(1, 4);
       
            if (banane == randomZahl)
            {
                foodThatFinoWantsEat = bananeObject; 
                FoodWishBanana.gameObject.SetActive(true);
            }

            else if (burger == randomZahl)
            {
                foodThatFinoWantsEat = burgerObject;
                FoodWishBurger.gameObject.SetActive(true);
            }

            else if (icecream == randomZahl)
            {
                foodThatFinoWantsEat = icecreamObject;
                FoodWishIceCream.gameObject.SetActive(true);
            }
        }
    }

    //Update bars
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

    //Meet needs
    public void FeedTheFino(GameObject food)
    {
        if (!gameOver)
        {
            if (food.name.Equals(foodThatFinoWantsEat?.name))
            {
                Debug.Log("Fino eats");
                hunger += 30;
            
                if (hunger > max)
                {
                    hunger = max;
                }

                UpdateHungerBar();
            }
            else
            {
                overrider.SetTrigger();
            }
        }
    }

    void PlayWithFino()
    {
        if (!gameOver)
        {
            Debug.Log("Play Button has been clicked"); 
            happiness += 20;
        
            if (happiness > max)
            {
            happiness = max;
            }

            UpdateHappyBar();
        }
    }

    public void SleepTheFino()
    {
        if (!gameOver)
        {
            Debug.Log("Sleep Button has been clicked"); 
 
            FinoHappy.gameObject.SetActive(false);
            FinoSleeps.gameObject.SetActive(true);
        }
    }

    public void AwakeTheFino()
    {
        if (!gameOver)
        {
            Debug.Log("Fino is awake again");

            FinoHappy.gameObject.SetActive(true);
            FinoSleeps.gameObject.SetActive(false);
        }
    }

    public void SocialTheFino()
    {
        if(!gameOver) 
        {
            Debug.Log("Teddy <3");
            social += (max-social);

            if (social > max)
            {
                social = max;
            }

            UpdateSocialBar();
        }
    }

    //GameOver Function
    void GameOver()
    {
        if (happiness == 0 && energy == 0 && hunger == 0 && social == 0)
        {
            gameOver = true;
            FinoHappy.gameObject.SetActive(false);
            FinoDead.gameObject.SetActive(true);
            happinessBubble.gameObject.SetActive(false);
            hungerBubble.gameObject.SetActive(false);
            socialBubble.gameObject.SetActive(false);
            energyBubble.gameObject.SetActive(false);
            thunderCloud.gameObject.SetActive(false);
            GameOverImage.gameObject.SetActive(true);
            FoodWishBanana.gameObject.SetActive(false);
            FoodWishBurger.gameObject.SetActive(false);
            FoodWishIceCream.gameObject.SetActive(false);

        }
    }
}
