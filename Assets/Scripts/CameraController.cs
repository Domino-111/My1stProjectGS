using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode
{
    FirstPerson,
    ThirdPerson
}

public class CameraController : MonoBehaviour
{
    private FirstPersonCamera firstPersonCamera;
    private ThirdPersonCamera thirdPersonCamera;
    private MoveCustom player;

    //SerializeField allows us to view and edit private variables in the Unity inspector. Serialize = convert to readable data, Field = the variable
    [SerializeField] private CameraMode currentCameraMode;

    void Start()
    {
        //Lock the mouse cursor and turn it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //FindObjectOfType will look for the first instance of the type provided in the scene. It does NOT look in inactive game objects by default.
        //We can pass "true" to include inactive objects
        firstPersonCamera = FindObjectOfType<FirstPersonCamera>(true);
        thirdPersonCamera = FindObjectOfType<ThirdPersonCamera>(true);
        player = FindObjectOfType<MoveCustom>(true);

        SetCameraMode();
    }

    void Update()
    {
        //KeyCode is an enumerator which contains all the keyboard keys
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCameras();
        }
    }

    //Swap from the current camera control to the other camera control
    private void ToggleCameras()
    {
        if (currentCameraMode == CameraMode.FirstPerson)
        {
            currentCameraMode = CameraMode.ThirdPerson;
        }

        else
        {
            currentCameraMode = CameraMode.FirstPerson;
        }

        SetCameraMode();
    }

    //Apply the camera mode to the game
    private void SetCameraMode()
    {
        if (currentCameraMode == CameraMode.FirstPerson)
        {
            firstPersonCamera.gameObject.SetActive(true);
            thirdPersonCamera.gameObject.SetActive(false);
        }

        else
        {
            firstPersonCamera.gameObject.SetActive(false);
            thirdPersonCamera.gameObject.SetActive(true);
        }

        player.SetCamera(currentCameraMode);
    }
}
