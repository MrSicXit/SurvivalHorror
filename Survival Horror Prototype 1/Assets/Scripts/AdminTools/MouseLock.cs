using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
    public KeyCode ToggleMouseLock;
    public bool IsCursorLocked;
    private bool CurrentCursorLockMode;
    // Start is called before the first frame update
    void Start()
    {
        CurrentCursorLockMode = IsCursorLocked;
        SetMouseLockState();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(ToggleMouseLock))
        {
            CurrentCursorLockMode = !CurrentCursorLockMode;
            SetMouseLockState();
        }
    }

    void SetMouseLockState()
    {
        if (CurrentCursorLockMode)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
