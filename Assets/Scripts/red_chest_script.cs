using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_chest_script : MonoBehaviour
{

    [SerializeField] GameObject open_chest_object;
    [SerializeField] AudioSource open_audio;

    private void open_chest(){
        open_audio.Play();
        Debug.Log("this chest is empty"); //give key
        Instantiate<GameObject>(open_chest_object, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Projectile"){
            open_chest();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
