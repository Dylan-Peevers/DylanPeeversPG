using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    float angle=0f, distance=5f, angle_D=-60;
    Transform owning_character_transform;
    private Player_Movement owning_character;
    private float sensitivity_vertical_rotate=0.01f;

    // Start is called before the first frame update
    void Start()
    {
        angle = angle_D * Mathf.Deg2Rad;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, distance * Mathf.Cos(angle), distance * Mathf.Sin(angle));
    }

    internal void adjust_vertical_angle(float vertical_adjustment)
    {
        angle += sensitivity_vertical_rotate * vertical_adjustment;
        angle = Mathf.Clamp(angle,-80*Mathf.Deg2Rad,-50*Mathf.Deg2Rad);
        angle_D = angle * Mathf.Rad2Deg;
    }

    internal void you_belong_to(Player_Movement player_Movement)
    {
        owning_character_transform = player_Movement.transform;
        owning_character = player_Movement;
    }
}
