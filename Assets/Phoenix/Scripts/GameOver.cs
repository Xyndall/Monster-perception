using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public bool EndGame = false;

    public void EndofGame()
    {
        //EndGame is true launch the Game Over Screen
        if (EndGame == false)
        {
            EndGame = true;
            GameOverScreen();
        }
    }
    //Send Player to the Next Scene
    void GameOverScreen()
    {
        SceneManager.LoadScene(3);
    }
}
