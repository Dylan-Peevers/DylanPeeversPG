using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Controller : MonoBehaviour
{

    internal enum Monster_States
    {
        Idle, Attacking, Dying, Dead, Move_to_Target
    }
    internal Monster_States current_state = Monster_States.Idle;
    internal Character_Controller current_target;

    private float current_speed;
    private float walking_speed = 10;
    private float running_speed = 12.5f;
    private float turning_speed = 180;
    private Animator mon_animation;

    internal float Melee_distance;
    internal int DPS;
    internal float attack_time_interval = 0.5f;
    internal float attack_timer;

    internal float dying_timer;
    internal float dying_time;

    internal int MHP = 1000;

    internal float max_distance_ranged = 10, min_distance_ranged = -10;

    internal void ImTheManager(Manager manager)
    {
        theManager = manager;
    }

    internal Manager theManager;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        current_speed = walking_speed;
        mon_animation = GetComponent<Animator>();
        MHP = 1000;
    }

    // Update is called once per frame
    void Update()
    {

        switch (current_state)
        {
            case Monster_States.Idle:
                mon_animation.SetBool("isWalking", false);

                if (current_target)
                {
                    current_state = Monster_States.Move_to_Target;
                    mon_animation.SetBool("isWalking", true);
                    mon_animation.SetBool("isRunning", false);
                }
                else
                {
                    current_target = theManager.whats_my_target(this);
                    if (current_target != null)
                    {
                        Vector3 from_me_to_target = current_target.transform.position - transform.position;
                        from_me_to_target = new Vector3(from_me_to_target.x, 0, from_me_to_target.z);
                        velocity = current_speed * from_me_to_target.normalized;
                        transform.LookAt(current_target.transform);
                        current_state = Monster_States.Move_to_Target;
                    }
                }
                break;

            case Monster_States.Move_to_Target:

                if (current_target != null)
                    if (within_range(current_target))
                    {
                        current_state = Monster_States.Attacking;
                        mon_animation.SetBool("isAttacking", true);
                        mon_animation.SetBool("isWalking", false);
                        attack_timer = 0;
                        velocity = Vector3.zero;
                    }
                    else
                    {
                        current_state = Monster_States.Idle;
                    }

                transform.position += velocity * Time.deltaTime;
                break;

            case Monster_States.Attacking:

                if (current_target)
                {
                    if (attack_timer <= 0f)
                    {
                        attack(DPS * (int)attack_time_interval);
                        attack_timer = attack_time_interval;
                    }
                }
                else
                    current_state = Monster_States.Idle;

                attack_timer -= Time.deltaTime;

                break;

            case Monster_States.Dead:

                break;

        }
    }

    internal void attack(int dmg)
    {
        current_target.takedamage(dmg);
    }

    public void assign_target(Character_Controller current_target)
    {
        if ((current_state == Monster_States.Idle) || (current_state == Monster_States.Move_to_Target))
        {
            Vector3 from_me_to_player = current_target.transform.position - transform.position;
            Vector3 direction = from_me_to_player.normalized;
            velocity = direction * current_speed;
            current_state = Monster_States.Move_to_Target;
        }
    }

    internal virtual bool within_range(Character_Controller current_target)
    {
        return (Vector3.Distance(transform.position, current_target.transform.position) > min_distance_ranged) &&
            (Vector3.Distance(transform.position, current_target.transform.position) < current_target.Melee_distance + max_distance_ranged);
    }

    public void takeDamage(int how_much_damage)
    {
        MHP -= how_much_damage;
        if (MHP <= 0)
        {
            current_state = Monster_States.Dead;
            mon_animation.SetBool("isDead", true);
            print("Monster Defeated");
        }
    }

    internal void destroyGameObject()
    {
        theManager.monster_been_destroyed(this);

        GameObject.Destroy(this.gameObject);
    }

}
