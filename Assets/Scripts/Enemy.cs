using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AddNonTriggerBoxCollider();
	}

    // creates a collision box on all enemies dynamically
    private void AddNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update () {
        
	}

    void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
    }
}
