using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    [SerializeField] GameObject player_prefab;
    GameObject[] enemies;

    /*
    - the number of the current room:
    - the names of the scenes are in the format "room_{room_x}{room_y}"
    - you start in "room_5050" and if you exit to the north you end up in room_5051
    - a more elegant way of doing this would be to use a 2D array of scenes and manipulate the indices to select 
        a scene to load
    */
    int room_x = 50;
    int room_y = 50;

    //the player's current coordinates
    [SerializeField] float player_x;
    [SerializeField] float player_y;

    /*this dictionary tracks the state of the rooms. this makes sure enemies and powerups are only spawned the first time 
    the room is loaded*/
    public Dictionary<string, bool> room_status = new Dictionary<string, bool>{
        {"room_5050", false},
        {"room_5051", false},
        {"room_4951", false},
        {"room_5151", false}
    };

    
    private static GameManager instance;
    public static GameManager Instance{
        get {
            if (instance == null){
                Debug.Log("GameManger is null");
            }
            return instance;
        }

    }
    
    GameObject get_player(){
        GameObject[] array_of_players = GameObject.FindGameObjectsWithTag("Player");
        if (array_of_players.Length == 0){
            Vector3 player_position = new Vector3(player_x, player_y, 0f);
            Quaternion player_rotation = Quaternion.Euler(0f, 0f, 0f);
            GameObject new_player = Instantiate<GameObject>(player_prefab,player_position, player_rotation);
            //update all the player attributes
            return new_player;
        }
        else{
            return array_of_players[0];
        }
    }

    void player_update(){
        GameObject the_player = get_player();
        player_x = the_player.transform.position.x;
        player_y = the_player.transform.position.y;
    }

    void check_for_exit(){
        if (player_y > 5.5f){exit_room("up");}          
        if (player_y < -8.2f){exit_room("down");}
        if (player_x < -8.3f){exit_room("left");}
        if (player_x > 12.3f){exit_room("right");}
    }

    void exit_room(string direction){
        if (direction == "up"){
            player_y = -6.5f;
            room_y++;    
        }
        if (direction == "down"){
            player_y = 3.7f;
            room_y--;    
        }
        if (direction == "left"){
            player_x = 10.5f;
            room_x--;    
        }
        if (direction == "right"){
            player_x = -6.5f;
            room_x++;    
        }

        string new_room = $"room_{room_x}{room_y}";
        SceneManager.LoadScene(new_room);
    }


    void check_for_completion(){
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0){
            string current_room_string = SceneManager.GetActiveScene().name;
            room_status[current_room_string] = true;
        }
    }


    void Awake(){
        if (instance == null) {instance = this;}
        else if (instance != this) {Destroy(gameObject);}

        DontDestroyOnLoad(gameObject);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        player_update();    //should this be in fixedupdates instead?
        check_for_exit();
        check_for_completion();
    }
}
