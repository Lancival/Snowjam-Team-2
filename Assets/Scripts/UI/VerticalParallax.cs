using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalParallax : MonoBehaviour
{
    private float PLAYER_MAX_HEIGHT = 9f;
    private float THIS_MAX_HEIGHT = 7f;

    private GameObject player;
    private float followRate;
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        followRate = THIS_MAX_HEIGHT / PLAYER_MAX_HEIGHT;
    }

    void Update()
    {
        if(player.transform.position.y * followRate < THIS_MAX_HEIGHT)
            this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y * followRate, this.transform.position.z);
    }
}
