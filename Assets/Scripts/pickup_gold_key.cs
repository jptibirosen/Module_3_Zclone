using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_gold_key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" 
            && GameManager.Instance.knows_the_secret 
            && !GameManager.Instance.has_gold_key)
        {
            GameManager.Instance.has_gold_key = true;
            GameManager.Instance.secret_audio.Play();
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
