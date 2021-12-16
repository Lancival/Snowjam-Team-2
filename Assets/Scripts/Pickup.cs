using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private enum PickupType{
        bread,
        butter,
        present,
        jam,
    }

    [SerializeField] private PickupType type;
    [SerializeField] private JamTypes jamType;
    [SerializeField] private List<Sprite> sprites;

    void Start()
    {
        if(type != PickupType.jam){
            jamType = JamTypes.None;
        }
        switch(type){
            case PickupType.bread:
                this.GetComponent<SpriteRenderer>().sprite = sprites[0];
                break;
            case PickupType.butter:
                Debug.LogError("Butter sprite not implemented");
                this.GetComponent<SpriteRenderer>().sprite = sprites[1];
                break;
            case PickupType.present:
                this.GetComponent<SpriteRenderer>().sprite = sprites[2];
                break;
            case PickupType.jam:
                switch(jamType){
                    case JamTypes.strawberry:
                        this.GetComponent<SpriteRenderer>().sprite = sprites[3];
                        break;
                    case JamTypes.avocado:
                        this.GetComponent<SpriteRenderer>().sprite = sprites[4];
                        break;
                    case JamTypes.peach:
                        Debug.LogError("Peach sprite not implemented");
                        this.GetComponent<SpriteRenderer>().sprite = sprites[5];
                        break;
                    default:
                        Debug.LogError("Error, Jam Type not set in the Inspector!");
                        break;
                }
                break;
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
