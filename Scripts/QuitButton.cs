using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // Cette fonction sera appelée lorsque le bouton sera cliqué
    public void Quit()
    {
        // Quitter le jeu lorsque l'application est buildée
        Application.Quit();

        // Si vous êtes dans l'éditeur Unity, arrêter le jeu
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
