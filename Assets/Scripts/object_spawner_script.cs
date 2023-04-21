using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class object_spawner_script : MonoBehaviour
{
    [SerializeField] GameObject player_prefab;
    [SerializeField] GameObject enemy_swordsman_prefab;
    [SerializeField] GameObject enemy_archer_prefab;

    void Awake() {
        string current_room_string = SceneManager.GetActiveScene().name;
        bool level_cleared = GameManager.Instance.room_status[current_room_string];
        if (!level_cleared){
            Instantiate<GameObject>(
                player_prefab, 
                new Vector3(0f, -6f, 0f),
                Quaternion.Euler(0f, 0f, 0f)
                );
        }        
    }


    // Start is called before the first frame update
    void Start()
    {
        string current_room_string = SceneManager.GetActiveScene().name;
        bool level_cleared = GameManager.Instance.room_status[current_room_string];
        if (!level_cleared){
            Instantiate<GameObject>(
                enemy_swordsman_prefab, 
                new Vector3(5.6f, 1.3f, 0f),
                Quaternion.Euler(0f, 0f, 0f)
                );
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
