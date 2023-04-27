using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_chest_script : MonoBehaviour
{

    [SerializeField] GameObject open_chest_object;
    [SerializeField] GameObject empty_message;
    [SerializeField] AudioSource open_audio;

    private void open_chest(){
        open_audio.Play();
        Debug.Log("this chest is empty"); //give key
        Instantiate<GameObject>(open_chest_object, transform.position, transform.rotation);
        GameObject new_message = Instantiate<GameObject>(empty_message);
        Destroy(new_message, 4f);
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
