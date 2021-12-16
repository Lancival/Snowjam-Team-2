using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duckling : MonoBehaviour
{
    [SerializeField] private int breadWanted;
    [SerializeField] private int butterWanted;
    [SerializeField] private int jamWanted;
    [SerializeField] private JamTypes jamType;

    private bool active;

    void Awake()
    {
        if(jamWanted > 0){
            if(jamType == JamTypes.None){
                Debug.LogError($"{this.name} wants jam but no Jam Type is set in the Inspector!");
            }
        }else{
            jamType = JamTypes.None;
        }
        active = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player" && other != other.gameObject.GetComponent<CircleCollider2D>()){
            if(active){
                PlayerControl player = other.gameObject.GetComponent<PlayerControl>();
                if( !player.presentHeld || breadWanted > player.breadHeld || butterWanted > player.butterHeld || (jamWanted > 0 && jamWanted > player.jamHeld[(int)jamType]) ){
                    Debug.Log($"Missing some stuff for {this.name}! Later this will be a bubble or something.");
                }else{
                    player.breadHeld -= breadWanted;
                    player.butterHeld -= butterWanted;
                    player.presentHeld = false;
                    if(jamWanted > 0){
                        player.jamHeld[(int)jamType] -= jamWanted;
                    }
                    active = false;
                    Debug.Log($"Successfully fed {this.name}!");
                }
            }else{
                Debug.Log($"{this.name} has already been fed! Later this will be text or something");
            }
        }
    }
}