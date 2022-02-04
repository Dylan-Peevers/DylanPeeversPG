using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    float angle=0f, distance=5f;
    Transform owning_character_transform;
    private Player_Movement owning_character;
    private float sensitivity_vertical_rotate=0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, distance * Mathf.Cos(angle), distance * Mathf.Sin(angle));
    }

    internal void adjust_vertical_angle(float vertical_adjustment)
    {
        angle += sensitivity_vertical_rotate * vertical_adjustment;
    }

    internal void you_belong_to(Player_Movement player_Movement)
    {
        owning_character_transform = player_Movement.transform;
        owning_character = player_Movement;
    }
}
