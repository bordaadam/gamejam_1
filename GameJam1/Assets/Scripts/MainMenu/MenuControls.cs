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


    public int width;
    public int height;
    public Text inputWidth;
    public Text inputHeight;
    public Slider Swidth;
    public Slider Sheight;


    public void MainMenu()
    {
        GameObject.Find("MainMenuCanvas").GetComponent<Canvas>().enabled = true;
    }

    public void StartGame()
    {
        width = (int)Swidth.value;
        height = (int)Sheight.value;
        SceneManager.LoadScene("Main");
        Debug.Log(width+" "+ height);
    }

    public void OptionsMenu()
    {
        GameObject.Find("MainMenuCanvas").GetComponent<Canvas>().enabled = false;
    }

    public void NewGameMenu()
    {
        GameObject.Find("MainMenuCanvas").GetComponent<Canvas>().enabled = false;
    }

    public void CreditsMenu()
    {
        
    }

    void Update()
    {
        inputWidth.text = "Width: " + Swidth.value;
        inputHeight.text = "Height: " + Sheight.value;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
