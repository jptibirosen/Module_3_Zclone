using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class room_5051_script : MonoBehaviour
{   
    [SerializeField] GameObject player_prefab;
    [SerializeField] GameObject enemy_swordsman_prefab;
    [SerializeField] GameObject enemy_archer_prefab;
    [SerializeField] GameObject door;   //these are the objects blocking the exits before enemies are cleared
    [SerializeField] GameObject purple_door;
    [SerializeField] GameObject purple_door_trigger;
    [SerializeField] GameObject green_door;
    [SerializeField] GameObject green_door_trigger;


    bool level_cleared(){
        string current_room_string = SceneManager.GetActiveScene().name;
        bool cleared = GameManager.Instance.room_status[current_room_string];
        return cleared;
    }

    void update_doors(){    //removes doors once enemies are cleared
        if (level_cleared()){
            GameObject[] array_of_doors = GameObject.FindGameObjectsWithTag("Door");
            List<GameObject> list_of_doors = new List<GameObject>();
            list_of_doors.AddRange(array_of_doors);

            for (int i = list_of_doors.Count - 1; i >= 0; i--)
            {
                Destroy(list_of_doors[i]);
            }
        }

    }

    void Awake() {
        
        //float player_x = GameManager.Instance.GetComponent("GameManager").player_x;
     
        //Vector3 player_position = new Vector3(0f, 0f, 0f);
        
        //Instantiate<GameObject>(player_prefab, player_position,Quaternion.Euler(0f, 0f, 0f));
        
    }

    // Start is called before the first frame update
    void Start(){
        if (!level_cleared()){
            Instantiate<GameObject>(enemy_swordsman_prefab, new Vector3(5.6f, 1.3f, 0f), Quaternion.Euler(0f, 0f, 0f));

            //south exit
            Instantiate<GameObject>(door, new Vector3(1.5f, -7.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate<GameObject>(door, new Vector3(2.5f, -7.5f, 0f), Quaternion.Euler(0f, 0f, 0f));            

            //east exit
            Instantiate<GameObject>(door, new Vector3(11.5f, -0.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate<GameObject>(door, new Vector3(11.5f, -1.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
        }

        if (!GameManager.Instance.opened_purple_door){
            //west exit
            Instantiate<GameObject>(purple_door, new Vector3(-7.5f, -0.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate<GameObject>(purple_door, new Vector3(-7.5f, -1.5f, 0f), Quaternion.Euler(0f, 0f, 0f));

            Instantiate<GameObject>(purple_door_trigger, new Vector3(-7f, -1f, 0f), Quaternion.Euler(0f, 0f, 0f));
        }

        if (!GameManager.Instance.opened_green_door){
            //north exit
            Instantiate<GameObject>(green_door, new Vector3(1.5f, 4.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate<GameObject>(green_door, new Vector3(2.5f, 4.5f, 0f), Quaternion.Euler(0f, 0f, 0f));

            Instantiate<GameObject>(green_door_trigger, new Vector3(2f, 4f, 0f), Quaternion.Euler(0f, 0f, 0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        update_doors();
    }
}
