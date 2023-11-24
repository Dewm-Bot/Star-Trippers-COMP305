using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameButton : MonoBehaviour
{
    public void PlayGameButtonPressed()
    {
        SceneManager.LoadScene(1);
    }
}
