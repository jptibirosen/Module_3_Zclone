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
    private int room_x = 50;
    private int room_y = 50;

    private int player_hp = 3;
    private int boss_hp = 3;

    //the player's current coordinates
    [SerializeField] public float player_x = 0f;
    [SerializeField] public float player_y = -6f;

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
    [SerializeField] GameObject heart;
    [SerializeField] GameObject boss_heart;
    [SerializeField] public AudioSource secret_audio;

    bool defeated = false;


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
    

    public void destroy_all(string tag){
        //destroys all game objects with a given tag

        GameObject[] array_of_objects = GameObject.FindGameObjectsWithTag(tag);
        for (int i = array_of_objects.Length - 1; i >= 0; i--){
            Destroy(array_of_objects[i]);
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
            destroy_all("Enemy");
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

        if (GameObject.FindGameObjectsWithTag("Heart").Length == 0){
            set_player_hp(0);   //used to draw hearts on the screen again
        }
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
        destroy_all(door_tag);
    }

    public int get_player_hp(){
        return player_hp;
    }

    public void set_player_hp(int difference){
        //update the player hp
        player_hp = Mathf.Clamp(player_hp + difference, 0, 3);

        //draw the hearts on the display
        destroy_all("Heart");
        for (int i = 0; i < player_hp; i++){
            Instantiate(heart, new Vector3(-11f + i, 4f, 0f), transform.rotation);
        }

        //end game if player is dead
        if (player_hp == 0 && !defeated){
            defeated = true;    //without this it would continue loading the scene
            SceneManager.LoadScene("room_lose");
        }
    }


    public int get_boss_hp(){
        return boss_hp;
    }

    public void set_boss_hp(int difference){
        //update the boss hp
        boss_hp = Mathf.Clamp(boss_hp + difference, 0, 5);

        //draw the hearts on the display
        if (SceneManager.GetActiveScene().name == "room_5057"){
            destroy_all("Boss_heart");
            for (int i = 0; i < boss_hp; i++){
                Instantiate(boss_heart, new Vector3(-11.5f, -2.5f + i, 0f), transform.rotation);
            }
        }
    }


    public void load_scene(string name){
        SceneManager.LoadScene(name);
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
        set_player_hp(3);
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
