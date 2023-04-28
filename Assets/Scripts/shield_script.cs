using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player_position = new Vector3(GameManager.Instance.player_x, GameManager.Instance.player_y, 0f);
        transform.position = player_position;
    }
}
