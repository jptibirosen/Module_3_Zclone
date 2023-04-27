using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class room_5056_script : MonoBehaviour
{

    [SerializeField] GameObject player_prefab;
    [SerializeField] GameObject gold_door;
    [SerializeField] GameObject gold_door_trigger;

    // Start is called before the first frame update
    void Start(){
    
    if (!GameManager.Instance.opened_gold_door){
            //north exit
            Instantiate<GameObject>(gold_door, new Vector3(1.5f, 4.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate<GameObject>(gold_door, new Vector3(2.5f, 4.5f, 0f), Quaternion.Euler(0f, 0f, 0f));

            Instantiate<GameObject>(gold_door_trigger, new Vector3(2f, 4f, 0f), Quaternion.Euler(0f, 0f, 0f));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
