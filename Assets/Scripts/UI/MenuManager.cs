using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void SinglePlayer()
    {
        SceneManager.LoadScene(2);
    }
    
    public void MultiPlayer()
    {
        SceneManager.LoadScene(1);
    }
    
    
}
