using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharacterHealth : MonoBehaviour
{
    internal int health = 1000, adjustHealth;
    public Text healthtext;

    Manager the_manager;
    Character_Controller theplayer;

    // Start is called before the first frame update
    void Start()
    {
        theplayer = gameObject.GetComponent<Character_Controller>();

    }

    // Update is called once per frame
    void Update()
    {

        health += adjustHealth;
        healthtext.text = "Health: " + health + "/1000";

        if (health < 0) {
            the_manager.player_died(theplayer);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            health--;
        }

    }

    internal void adjust_health(int adjustment)
    {
        adjustHealth = health += adjustment;

 //       if (health < 0)
 //           the_manager.player_died(theplayer);
    }

}
