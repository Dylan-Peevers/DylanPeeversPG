using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    List<Monster_Controller> AllMonsters;
    Character_Controller theplayer;
    CharacterHealth CHP;

    // Start is called before the first frame update
    void Start()
    {
        AllMonsters = FindObjectsOfType<Monster_Controller>().ToList();
        theplayer = FindObjectOfType<Character_Controller>();
        foreach(Monster_Controller monster in AllMonsters)
        {
            monster.ImTheManager(this);
        } 
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal Character_Controller whats_my_target(Monster_Controller searching_character)
    {
        if (searching_character is Character_Controller)
        {
            float distance = 100f;
            Character_Controller character = theplayer;
            foreach (Monster_Controller monster in AllMonsters)
            {
                if ((monster.current_state != Monster_Controller.Monster_States.Dying) && (monster.current_state != Monster_Controller.Monster_States.Dead))
                {
                    if (Vector3.Distance(character.transform.position, monster.transform.position) < distance)
                    {
                        distance = Vector3.Distance(character.transform.position, monster.transform.position);
                    }
                }

            }
            return character;
        }
        else 
        {
            float distance = 100f;
            Monster_Controller monster = searching_character;
            Character_Controller character = theplayer;
                if (Vector3.Distance(monster.transform.position, character.transform.position) < distance)
                {
                  //  distance = Vector3.Distance(monster.transform.position, character.transform.position);
                return character;
            }

            return null;
        }
    }

    internal void monster_been_destroyed(Monster_Controller monster_destroyed)
    {
        AllMonsters.Remove(monster_destroyed);
    }

    internal void player_died(Character_Controller player_dead)
    {
        if (CHP.health < 0) { 
                
        }


    }
}
