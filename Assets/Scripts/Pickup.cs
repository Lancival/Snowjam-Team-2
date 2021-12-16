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

    private PlayerControl pickupSprites;

    void Start()
    {
        pickupSprites = GameObject.Find("Player").GetComponent<PlayerControl>();
        if(type != PickupType.jam){
            jamType = JamTypes.None;
        }
        switch(type){
            case PickupType.bread:
                this.GetComponent<SpriteRenderer>().sprite = pickupSprites.sprites[0];
                break;
            case PickupType.butter:
                this.GetComponent<SpriteRenderer>().sprite = pickupSprites.sprites[1];
                break;
            case PickupType.present:
                this.GetComponent<SpriteRenderer>().sprite = pickupSprites.sprites[2];
                break;
            case PickupType.jam:
                switch(jamType){
                    case JamTypes.strawberry:
                        this.GetComponent<SpriteRenderer>().sprite = pickupSprites.sprites[3];
                        break;
                    case JamTypes.avocado:
                        this.GetComponent<SpriteRenderer>().sprite = pickupSprites.sprites[4];
                        break;
                    case JamTypes.peach:
                        this.GetComponent<SpriteRenderer>().sprite = pickupSprites.sprites[5];
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
            Debug.Log($"Picking up object {this.name} ...");
            PlayerControl player = other.gameObject.GetComponent<PlayerControl>();
            switch(type){
                case PickupType.bread:
                    player.breadHeld += 1;
                    break;
                case PickupType.butter:
                    player.butterHeld += 1;
                    break;
                case PickupType.present:
                    player.presentHeld = true;
                    break;
                case PickupType.jam:
                    player.jamHeld[(int)jamType] += 1;
                    break;    
            }
            // player.UpdateDisplay();
            Destroy(this.gameObject);  
        }
    }
}
