using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    [Header("Assignables")]
    [Range(1, 50)]
    [SerializeField] private float mouseSpeed = 20;
    [SerializeField] private GameObject ThePlayer;

    //private variables
    private float xRot;

    private void Start()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //input
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime * 5;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime * 5;
        //camera rotation
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        //player rotation
        ThePlayer.transform.Rotate(Vector3.up * mouseX);
    }
}