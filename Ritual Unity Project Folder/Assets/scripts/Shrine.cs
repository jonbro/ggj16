﻿using UnityEngine;
using System.Collections;

public class Shrine : MonoBehaviour {
	bool hasPlayer, hasObject;
	public GameObject objectPosition;
	public GameObject objectForPickup;

	public BoxCollider pickupTrigger, mainTrigger;
	void Update(){
		if(hasPlayer && Input.GetMouseButtonDown(0)){
			if(GameController.instance.holdingObject.holdingObject != null && !hasObject){
				GameObject holdObject = GameController.instance.holdingObject.holdingObject;
				holdObject.transform.position = objectPosition.transform.position;
				holdObject.transform.SetParent(objectPosition.transform);
				GameController.instance.holdingObject.RemoveHoldingObject();
				hasObject = true;
				// if we have a seperate pickup trigger, then activate that
				if(pickupTrigger != null){
					mainTrigger.enabled = false;
					pickupTrigger.enabled = true;
					hasPlayer = false;
				}
				SendMessage("ObjectPlaced", holdObject);
			}else if(objectForPickup != null){
				hasObject = false;
				GameController.instance.holdingObject.SetHoldingObject(objectForPickup);
				objectForPickup = null;
				if(pickupTrigger != null){
					mainTrigger.enabled = true;
					pickupTrigger.enabled = false;
					hasPlayer = false;
				}
			}
		}
	}
	void RitualComplete(GameObject target){
		objectForPickup = target;
		if(objectForPickup.GetComponent<Ritualized>() == null){
			objectForPickup.AddComponent<Ritualized>();
		}
	}
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			hasPlayer = true;
		}
	}
	void OnTriggerExit(Collider other) {
		if(other.tag == "Player"){
			hasPlayer = false;
		}
	}
}
