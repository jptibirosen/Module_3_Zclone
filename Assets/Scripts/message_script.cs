using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class message_script : MonoBehaviour
{
    [SerializeField] GameObject message;
    [SerializeField] GameObject face;


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            GameObject new_message = Instantiate<GameObject>(message);
            Destroy(new_message, 4f);

            GameObject new_face = Instantiate<GameObject>(face);
            Destroy(new_face, 4f);
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
