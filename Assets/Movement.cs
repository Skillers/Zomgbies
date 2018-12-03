using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public SphereCollider soundArea;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftControl))
        {
            this.transform.position += Vector3.forward * Time.deltaTime;
            soundArea.radius = 2f;
        }else if(Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift))
        {
            this.transform.position += Vector3.forward * Time.deltaTime;
            soundArea.radius = 10f;
        }else if (Input.GetKey("w"))
        {
            this.transform.position += Vector3.forward * Time.deltaTime;
            soundArea.radius = 4f;
        }else
        {
            soundArea.radius = 0.5f;
        }
	}
}
