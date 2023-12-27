using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class finishLevel : MonoBehaviour
{
    [SerializeField] GameObject finishMenu;
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    public Behaviour Player;
    public Behaviour Gunmuzzle;
    public string sceneName;





    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Player.enabled = false;
            Gunmuzzle.enabled = false;
            finishMenu.SetActive(true);
            Time.timeScale = 0;
            
            Debug.Log(message: $"Your Time is {timerText.text}", gameObject);
            Debug.Log(message: $"Level {sceneName} Finished", gameObject);
        }
        else
        {
            finishMenu.SetActive(false);
            Time.timeScale = 1;
            Player.enabled = true;
            Gunmuzzle.enabled = true;
        }
    }
    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
        Player.enabled = false;
        Gunmuzzle.enabled = false;
    }
     public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
        Player.enabled = false;
        Gunmuzzle.enabled = false;
    }


    void Update()
    {
        elapsedTime += Time.deltaTime;
        timerText.text = elapsedTime.ToString();
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

    }


}
