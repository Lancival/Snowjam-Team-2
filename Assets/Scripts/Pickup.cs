using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private enum PickupType{
        bread,
        butter,
        jam,
    }

    [SerializeField] private PickupType type;
    [SerializeField] private JamTypes jamType;

    void Start()
    {
        switch(type){
            case PickupType.bread:
                // this.GetComponent<SpriteRender>)().sprite =
                break;
            case PickupType.butter:
                // this.GetComponent<SpriteRender>)().sprite =
                break;
            case PickupType.jam:
                // this.GetComponent<SpriteRender>)().sprite =
                break;
        }
        if(type != PickupType.jam){
            jamType = JamTypes.None;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player"){
            Debug.Log("Picking up object...");
            switch(type){
                case PickupType.bread:
                    other.gameObject.GetComponent<PlayerControl>().breadHeld += 1;
                    break;
                case PickupType.butter:
                    other.gameObject.GetComponent<PlayerControl>().butterHeld += 1;
                    break;
                case PickupType.jam:
                    other.gameObject.GetComponent<PlayerControl>().jamHeld[(int)jamType] += 1;
                    break;    
            }
            Destroy(this.gameObject);  
        }
    }
}
