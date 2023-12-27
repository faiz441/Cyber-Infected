
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float fullHealth;
    float currentHealth;
  

    public GameObject playerDeadFX;


    //HUD
    public Slider playerHealthSlider;
    public Image damageScreen;
    Color flashColor = new Color(255f, 255f, 255f,0.5f);
    float flashSpeed = 3f;
    bool damaged = false;
    public Text endGameText;
    public restartGame theGameController;
 
    AudioSource playerAS;


    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = fullHealth;
        playerHealthSlider.maxValue = fullHealth;
        playerHealthSlider.value = currentHealth;

        playerAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Hurt/not
        if (damaged)
        {
            damageScreen.color = flashColor;
        }
        else
        {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, flashSpeed*Time.deltaTime);
        }
       
        damaged = false;
    }

    public void addDamage(float damage)
    {
        currentHealth -= damage;
        playerHealthSlider.value = currentHealth;
        damaged = true;
      
       playerAS.Play();
      
        Debug.Log(message: $"Hit by Enemy for {damage} damage", gameObject);
        if (currentHealth <= 0)
        {
            makeDead();
        }
    }

    public void addHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > fullHealth) currentHealth = fullHealth;
        playerHealthSlider.value = currentHealth;
        Debug.Log(message:$"Player healed for {health} health", gameObject);
    }


    public void makeDead()
    {
        Instantiate(playerDeadFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        damageScreen.color = flashColor;
        Destroy(gameObject);
        playerHealthSlider.value = 0;
        Animator endGameAnim = endGameText.GetComponent<Animator>();
        endGameAnim.SetTrigger("endGame");
        theGameController.restartTheGame();
        Debug.Log(message: $"Player Dead", gameObject);
    }

    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
