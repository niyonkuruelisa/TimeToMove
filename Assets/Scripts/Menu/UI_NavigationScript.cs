using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_NavigationScript : MonoBehaviour
{



    //LEVELS SCENES
    //Reload Level
    public void ReloadScene()
    {

        CharacterBehaviourScript.dead = false;
        //fully reload Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    //Level Menu
    // MENU BUTTON - Load MENU Scene
    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // LEVEL 1 BUTTON - Load LEVEL 1 Scene
    public void OnLevel1ButtonClicked()
    {
        SceneManager.LoadScene("Level_1");
    }


    //Main Menu
    // PLAY BUTTON - Load Level Scene
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("MenuLevels");
    }

    // SETTINGS BUTTON - Load SETTINGS Scene
    public void OnSettingsButtonClicked()
    {
        SceneManager.LoadScene("Settings");
    }

    // HELP BUTTON - Load HELP Scene
    public void OnHelpButtonClicked()
    {
        SceneManager.LoadScene("Help");
    }

    // QUIT BUTTON - CLOSE GAME
    public void OnQuitButtonClicked()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Application.Quit();
        }
    }
}
