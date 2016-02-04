﻿using UnityEngine;
using System.Collections;

public class ModelRotationController : MonoBehaviour {
    private Vector3 moveDirection = Vector3.zero;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveDirection);

    }
}