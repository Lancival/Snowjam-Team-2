using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duckling : MonoBehaviour
{
    [SerializeField] private int breadWant;
    [SerializeField] private int butterWant;
    [SerializeField] private int jamWanted;
    [SerializeField] private JamTypes jamType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player"){
            PlayerControl player = other.gameObject.GetComponent<PlayerControl>();
            if( breadWant > player.breadHeld || butterWant > player.butterHeld || (jamWanted && jamWanted > player.jamHeld[jamType]) ){
                Debug.Log("Successfully fed duckling! Later this should probably be a bubble");
            }else{
                Debug.Log("Missing some stuff! Later this should be a bubble or something.");
            }
        }
    }
}