using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    public event UnityAction<bool> TimeStopped;

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        TimeStopped?.Invoke(true);
        //Time.timeScale = 0;
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        TimeStopped?.Invoke(false);
        //Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
