using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setup : MonoBehaviour
{
    public void GoNext()
    {
        //Goes to setup scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}


