using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_attributes : MonoBehaviour
{   
    [SerializeField] int player_health;
    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D col) {
        //Debug.Log("player was hit");
        if (col.gameObject.tag == "Projectile"){
            //Destroy(col.gameObject);
            Debug.Log("player was hit by projectile");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
