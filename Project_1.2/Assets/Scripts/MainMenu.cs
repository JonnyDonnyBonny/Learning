using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void TaskOnClick()
    {
        Application.Quit();
        Debug.Log("���������� �������");
    }

    public void ScriptOj()
    {
        SceneManager.LoadScene(2);
    }
}
