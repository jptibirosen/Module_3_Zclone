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
    [SerializeField] float player_x = 0f;
    [SerializeField] float player_y = -6f;

    [SerializeField] public bool has_purple_key = false;
    [SerializeField] public bool has_green_key = false;
    [SerializeField] public bool has_gold_key = false;
    [SerializeField] public bool opened_purple_door = false;
    [SerializeField] public bool opened_green_door = false;
    [SerializeField] public bool opened_gold_door = false;
    [SerializeField] public bool has_acorns = false;
    [SerializeField] public bool knows_the_secret = false;
    [SerializeField] GameObject purple_key_message;
    [SerializeField] GameObject green_key_message;
    [SerializeField] GameObject gold_key_message;
    [SerializeField] GameObject acorns_message;
    [SerializeField] public AudioSource secret_audio;


    /*this dictionary tracks the state of the rooms. this makes sure enemies and powerups are only spawned the first time 
    the room is loaded*/
    public Dictionary<string, bool> room_status = new Dictionary<string, bool>{
        {"room_5050", false},
        {"room_5051", false},
        {"room_5151", false},
        {"room_5251", false},
        {"room_4951", false},
        {"room_4851", false},
        {"room_4751", false},
        {"room_5052", false},
        {"room_5053", false},
        {"room_5054", false},
        {"room_5055", false},
        {"room_5056", false},
        {"room_5057", false},
        {"room_5156", false},
        {"room_5256", false},
        {"room_5356", false},
        {"room_5355", false},
        {"room_4752", false}  
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

    private void ask_status(){
        if (Input.GetKeyDown(KeyCode.O)){
            Debug.Log($"has purple key: {has_purple_key}");
            Debug.Log($"has green key: {has_green_key}");
            Debug.Log($"has gold key: {has_gold_key}");
            Debug.Log($"opened purple door: {opened_purple_door}");
            Debug.Log($"opened green door: {opened_green_door}");
            Debug.Log($"opened gold door: {opened_gold_door}");
            Debug.Log($"has acorns: {has_acorns}");
            Debug.Log($"knows the secret: {knows_the_secret}");
        }

        if(Input.GetKeyDown(KeyCode.D)){
            GameObject[] array_of_enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = array_of_enemies.Length - 1; i >= 0; i--)
            {
                Destroy(array_of_enemies[i]);
            }
        }
    }

    void player_update(){
        GameObject the_player = get_player();
        player_x = the_player.transform.position.x;
        player_y = the_player.transform.position.y;
    }

    /*public void display_message(string name){

        Dictionary<string, GameObject> messages = new Dictionary<string, GameObject>{
            {"purple_key", purple_key_message},
            {"green_key", green_key_message},
            {"gold_key", gold_key_message},
            {"acorns", acorns_message}
        };
        Instantiate<GameObject>(messages[name]);
    }*/

    void inventory_display_update(){
        GameObject purple_message = GameObject.Find("purple_key_message(Clone)");
        if(has_purple_key && purple_message == null){Instantiate<GameObject>(purple_key_message);;}

        GameObject green_message = GameObject.Find("green_key_message(Clone)");
        if(has_green_key && green_message == null){Instantiate<GameObject>(green_key_message);;}

        GameObject gold_message = GameObject.Find("gold_key_message(Clone)");
        if(has_gold_key && gold_message == null){Instantiate<GameObject>(gold_key_message);;}

        GameObject aco_message = GameObject.Find("acorn_message(Clone)");
        if(has_acorns && aco_message == null){Instantiate<GameObject>(acorns_message);;}
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

    public void open_door(string color){
        /* takes a sting argument starting wiht a capital letter (eg. "Purple")
        removes all doors in the scene tagged with that color*/
        
        //Debug.Log($"open_door(\"{color}\") was called");
        string door_tag = $"{color}_door";
        GameObject[] array_of_doors = GameObject.FindGameObjectsWithTag(door_tag);
        List<GameObject> list_of_doors = new List<GameObject>();
        list_of_doors.AddRange(array_of_doors);

        for (int i = list_of_doors.Count -1 ; i >= 0 ; i--)
        {
            Destroy(list_of_doors[i]);
        }


    }


    void Awake(){
        if (instance == null) {instance = this;}
        else if (instance != this) {Destroy(gameObject);}

        DontDestroyOnLoad(gameObject);
        player_update();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {   
        ask_status();
        inventory_display_update();
        player_update();    //should this be in fixedupdates instead?
        check_for_exit();
        check_for_completion();
        
    }
}
