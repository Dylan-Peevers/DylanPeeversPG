using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Behavior : MonoBehaviour
{

    internal enum Unit_States
    {
        Idle, Attacking, Dying, Dead, Move_to_Target
    }
    internal Unit_States current_state = Unit_States.Idle;

    private float current_speed;
    private float walking_speed = 5;
    private float running_speed = 10;
    private float turning_speed = 180;
    private Animator mon_animation;

    // Start is called before the first frame update
    void Start()
    {
        current_speed = walking_speed;
        mon_animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    mon_animation.SetBool("isRunning", true);
                    current_speed = running_speed;
                }
                transform.position += walking_speed * transform.forward * Time.deltaTime;
        */
    }

}
