using UnityEngine;
using System.Collections;

public class light_delayed : MonoBehaviour {
	public float delay;
	public float max_intensity;
	public float reduction;
	private bool state = false;
	
	// Use this for initialization
	void Start () {
	GetComponent<Light>().intensity = 0f;
		 Invoke("Light_start", delay);
		
	}
	
	void Light_start(){
		state = true;
		GetComponent<Light>().intensity = max_intensity+max_intensity/4f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!state) {
	GetComponent<Light>().intensity+= (max_intensity - GetComponent<Light>().intensity)/(5f*reduction);
		} else {
	GetComponent<Light>().intensity+= (- GetComponent<Light>().intensity)/reduction;
		}
	}
}
