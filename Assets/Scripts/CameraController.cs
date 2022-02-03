using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSens;
    [SerializeField] private Transform player;
    float xAxisClamp;

    private PlayerStats stats;

    private void Start() 
    {
        GetReference();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSens;
        float rotAmountY = mouseY * mouseSens;

        xAxisClamp -= rotAmountX;

        Vector3 rotPlayer = player.transform.rotation.eulerAngles;
        
        rotPlayer.y += rotAmountX;
        rotPlayer.x -= rotAmountY;

        if(xAxisClamp > 90)
        {
            xAxisClamp = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
        }

        if(!stats.IsDead())
            player.rotation = Quaternion.Euler(rotPlayer);
        else if(Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;

    }

    private void GetReference()
    {
        stats = GetComponentInParent<PlayerStats>();
    }
}
