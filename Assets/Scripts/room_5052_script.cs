using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class room_5052_script : MonoBehaviour
{
    [SerializeField] GameObject enemy_swordsman_prefab;
    [SerializeField] GameObject enemy_archer_prefab;
    [SerializeField] GameObject door;   //these are the objects blocking the exits before enemies are cleared


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

    // Start is called before the first frame update
    void Start()
    {
        string current_room_string = SceneManager.GetActiveScene().name;
        bool level_cleared = GameManager.Instance.room_status[current_room_string];
        if (!level_cleared){
            Instantiate<GameObject>(enemy_swordsman_prefab, new Vector3(1.4f, 1.87f, 0f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate<GameObject>(enemy_archer_prefab, new Vector3(5.46f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));

            //north exit
            Instantiate<GameObject>(door, new Vector3(1.5f, 4.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate<GameObject>(door, new Vector3(2.5f, 4.5f, 0f), Quaternion.Euler(0f, 0f, 0f));

            //south exit
            Instantiate<GameObject>(door, new Vector3(1.5f, -7.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate<GameObject>(door, new Vector3(2.5f, -7.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        update_doors();
    }
}
