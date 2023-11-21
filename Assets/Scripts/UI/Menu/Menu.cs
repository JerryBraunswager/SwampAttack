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
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        TimeStopped?.Invoke(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
