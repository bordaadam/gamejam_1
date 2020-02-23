using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuControls : MonoBehaviour

{
    private void Start()
    {
        DontDestroyOnLoad(this);
        
    }


    public static int width;
    public static int height;
    public Text inputWidth;
    public Text inputHeight;


    public void MainMenu()
    {
        GameObject.Find("MainMenuCanvas").GetComponent<Canvas>().enabled = true;
        GameObject.Find("NewGameCanvas").GetComponent<Canvas>().enabled = false;
        GameObject.Find("Options").GetComponent<Canvas>().enabled = false;
    }

    public void StartGame()
    {
        width = int.Parse(inputWidth.text);
        height = int.Parse(inputHeight.text);
        SceneManager.LoadScene("Main");
        Debug.Log(width+" "+ height);
    }

    public void OptionsMenu()
    {
        GameObject.Find("Options").GetComponent<Canvas>().enabled = true; ;
        GameObject.Find("MainMenuCanvas").GetComponent<Canvas>().enabled = false;
    }

    public void NewGameMenu()
    {
        GameObject.Find("MainMenuCanvas").GetComponent<Canvas>().enabled = false;
        GameObject.Find("NewGameCanvas").GetComponent<Canvas>().enabled = true; ;
    }

    public void CreditsMenu()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
