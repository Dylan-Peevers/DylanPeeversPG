using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    private float current_speed;
    private float turning_speed = 180;
    private float turning_sensitivity = 20;
    private float elevation_angle = 0;
    Camera_Controller my_camera;
    Animator char_animation;
    private float walking_speed = 5, sprint_speed = 7.5f;
    internal float Melee_distance = 1.5f;

    Manager theManager;
    CharacterHealth health;

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
        char_animation.SetBool("isIdle", true);
        char_animation.SetBool("isWalking", false);
        char_animation.SetBool("isSprinting",false);
        char_animation.SetBool("isAttack1",false);
        char_animation.SetBool("isAttack2", false);
        char_animation.SetBool("isDead", false);

        // Implement motion
        if (should_move_forward()) move_forward();
        if (should_move_backward()) move_backward();
        if (should_strafe_left()) strafe_left();
        if (should_strafe_right()) strafe_right();

        if (should_sprint()) sprint();

        //Implement Attack

        if (should_attack1()) player_attack1();
        if (should_attack2()) player_attack2();


        turn(Input.GetAxis("Horizontal"));
        adjust_camera(Input.GetAxis("Vertical"));

        elevation_angle -= Input.GetAxis("Vertical");
        elevation_angle = Mathf.Clamp(elevation_angle, 85f, 85f);


        Convert.ToInt32(health);
    }

    private void adjust_camera(float vertical_adjustment)
    {
        my_camera.adjust_vertical_angle(vertical_adjustment);
    }

    internal void destroyGameObject()
    {
        theManager.player_died(this);

        GameObject.Destroy(this.gameObject);
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
        char_animation.SetBool("isWalking", true);
        char_animation.SetBool("isIdle", false);
        transform.position += current_speed * transform.right * Time.deltaTime;
    }


    private void strafe_left()
    {
        char_animation.SetBool("isWalking", true);
        char_animation.SetBool("isIdle", false);
        transform.position -= current_speed * transform.right * Time.deltaTime;
    }

    /// <summary>
    /// Move the gameobject forward relative to its own orientation
    /// </summary>
    private void move_forward()
    {
        char_animation.SetBool("isWalking",true);
        char_animation.SetBool("isIdle", false);
        transform.position += current_speed * transform.forward * Time.deltaTime;
    }
    private void move_backward()
    {
        char_animation.SetBool("isWalking", true);
        char_animation.SetBool("isIdle", false);
        transform.position -= current_speed * transform.forward * Time.deltaTime;
    }

    private void sprint()
    {
        char_animation.SetBool("isSprinting", true);
        char_animation.SetBool("isWalking", false);
        char_animation.SetBool("isIdle", false);
        transform.position += sprint_speed * transform.forward * Time.deltaTime;
    }

    //Attack Methods

    private void player_attack1()
    {
        char_animation.SetBool("isAttack1", true);
        char_animation.SetBool("isIdle", false);
    }

    private void player_attack2()
    {
        char_animation.SetBool("isAttack2", true);
        char_animation.SetBool("isIdle", false);
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

    private bool should_sprint()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    //User input for attacks
    private bool should_attack1()
    {
        return Input.GetMouseButtonDown(0);
    }

    private bool should_attack2()
    {
        return Input.GetMouseButtonDown(1);
    }

    //Implements Damage to character health

    void registerHit(int hitDamage)
    {
        Ray attack = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(attack, out hit))
        {
            CharacterHealth characterHealth = hit.collider.gameObject.GetComponent<CharacterHealth>();

            if (characterHealth)
                characterHealth.adjust_health(hitDamage);
        }
    }

    internal void takedamage(int dmg)
    {
            registerHit(dmg);
    }
}
