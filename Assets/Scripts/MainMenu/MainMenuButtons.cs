using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour {

    public void ButtonPlay()
    {
        Application.LoadLevel(1);
    }

    public void ButtonExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
