using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float current_speed;
    private float turning_speed = 180;
    private float turning_sensitivity = 20;
    private float elevation_angle = 0;
    Camera_Controller my_camera;
    Animator char_animation;
    private float walking_speed = 1, sprint_speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        current_speed = walking_speed;
        char_animation = GetComponent<Animator>();

        my_camera = Camera.main.GetComponent<Camera_Controller>();
        my_camera.you_belong_to(this);
        
        
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Animations
        char_animation.SetBool("Walking", false);


        // Implement motion
        if (should_move_forward()) move_forward();
        if (should_move_backward()) move_backward();
        if (should_strafe_left()) strafe_left();
        if (should_strafe_right()) strafe_right();

        turn(Input.GetAxis("Horizontal"));
        adjust_camera(Input.GetAxis("Vertical"));

        elevation_angle -= Input.GetAxis("Vertical");
        elevation_angle = Mathf.Clamp(elevation_angle, -85f, 85f);
    }

    private void adjust_camera(float vertical_adjustment)
    {
        my_camera.adjust_vertical_angle(vertical_adjustment);
    }

    // Movement methods

    private void turn(float Input_axis_value)
    {
        transform.Rotate(Vector3.up, turning_sensitivity * Input_axis_value * Time.deltaTime);
    }
    private void turn_right()
    {
        transform.Rotate(Vector3.up, turning_speed * Time.deltaTime);
    }

    private void turn_left()
    {
        transform.Rotate(Vector3.up, -turning_speed * Time.deltaTime);
    }


    private void strafe_right()
    {

        transform.position += current_speed * transform.right * Time.deltaTime;
    }


    private void strafe_left()
    {
        transform.position -= current_speed * transform.right * Time.deltaTime;
    }

    /// <summary>
    /// Move the gameobject forward relative to its own orientation
    /// </summary>
    private void move_forward()
    {
        char_animation.SetBool("Walking",true);
        transform.position += current_speed * transform.forward * Time.deltaTime;
    }
    private void move_backward()
    {
        transform.position -= current_speed * transform.forward * Time.deltaTime;
    }

    // User input for movement
    private bool should_move_forward()
    {
        return Input.GetKey(KeyCode.W);
    }

    private bool should_move_backward()
    {
        return Input.GetKey(KeyCode.S);
    }

    private bool should_strafe_right()
    {
        return Input.GetKey(KeyCode.D);
    }

    private bool should_strafe_left()
    {
        return Input.GetKey(KeyCode.A);
    }
}
