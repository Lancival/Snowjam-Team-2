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
    private float cycleTime = .75f;
    private float timer;
    private bool up;

    void Start()
    {   
        timer = 0;
        up = true;
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
            other.GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);  
        }
    }

    void FixedUpdate(){
        if(up){
            this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y + .01f, 0);
            timer += Time.deltaTime;
            if(timer > cycleTime) up = false; 
        }else{
            this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y - .01f, 0);
            timer -= Time.deltaTime;
            if(timer < 0) up = true;
        }
    }
}
