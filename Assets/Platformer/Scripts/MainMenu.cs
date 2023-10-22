using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{

    public class MainMenu : MonoBehaviour
    {
        public void StartMenu()
        {
            SceneManager.LoadScene("lvl1");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
