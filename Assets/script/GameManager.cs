using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{

    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
        // Utilisez UnityEngine.SceneManagement pour spécifier le SceneManager de Unity.
    }

    public void LoadExit()
    {
        UnityEngine.Application.Quit();
        // Utilisez UnityEngine.Application pour spécifier l'Application de Unity.
    }
}

