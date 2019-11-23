using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnPause = null;

    [SerializeField]
    UnityEvent OnUnpause = null;

    bool isPaused = false; 

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !isPaused)
        {
            Pause();
        }
    }

    void Pause()
    {
        if (isPaused)
        {
            Debug.LogWarning("Tried to Pause whilst already paused!");
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0;
            OnPause.Invoke();
        }
    }

    public void Unpause()
    {
        if (!isPaused)
        {
            Debug.LogWarning("Tried to Unpause whilst already unpaused!");
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            OnUnpause.Invoke();
        }
    }

    private void OnDestroy()
    {
        if (isPaused)
        {
            Unpause();
        }
    }
}
