using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHelper 
{
    public static void LoadScene(Scenes scenes)
    {
        SceneManager.LoadScene((int)scenes);
    }
}

public enum Scenes
{
   Menu, 
   Game
}
