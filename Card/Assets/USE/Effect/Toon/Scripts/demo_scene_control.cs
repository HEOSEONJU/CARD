using UnityEngine;
using System.Collections;

public class demo_scene_control : MonoBehaviour {
	public GameObject explosion;
	public GameObject gr_explosion;
	public GameObject flash_expl;
	public GameObject gr_flash_expl;
	public GameObject debris_explosion;
	
	
	public GameObject debris;
	public GameObject huge_expl;
	public GameObject expl_w_t;
	public GameObject expl_sign;
	public GameObject smoke;
	public GameObject flash_2;
	public GameObject flash_3;
	public GameObject flash_3w;
	public GameObject wave;
	public GameObject wave_expl;
	public GameObject gr_hit;
	public GameObject stunt;
	public GameObject restore_health;
	public GameObject blessing;
	public GameObject curse;
	public GameObject lightning;
	public GameObject fireball;
	public GameObject d_fireball;
	public GameObject fire;
	public GameObject fire_sm;
	public GameObject poison;
	public GameObject curse_2;
	public GameObject attack_upgr;
	public GameObject armor_broken;
	public GameObject upgrade;
	public GameObject sleep;
	
	private Transform expl_spawn;
	private Transform h_expl_spawn;
	private Transform small_expl_spawn;
	private GameObject active;
	private float x_angle=0f;
	// Use this for initialization
	void Start () {
		
	expl_spawn = GameObject.Find("expl_spawn").transform;
	h_expl_spawn = GameObject.Find("h_expl_spawn").transform;
	this.transform.LookAt(expl_spawn.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}
	
	void OnGUI(){
		if (GUI.Button(new Rect(20,20,200,20),"Basic Explosion")){
		GameObject g_explosion = Instantiate(explosion,expl_spawn.position+new Vector3(0f,1f,0f),expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_explosion;
		}
		if (GUI.Button(new Rect(20,50,200,20),"Ground Explosion")){
		GameObject g_gr_explosion = Instantiate(gr_explosion,expl_spawn.position,expl_spawn.rotation) as GameObject;
		Destroy(active);
			active=g_gr_explosion;
		}
		if (GUI.Button(new Rect(20,80,200,20),"Flash Explosion")){
		GameObject g_flash_expl = Instantiate(flash_expl,expl_spawn.position+new Vector3(0f,1f,0f),expl_spawn.rotation) as GameObject;
		Destroy(active);
			active=g_flash_expl;
		}
		if (GUI.Button(new Rect(20,110,200,20),"Ground Flash Explosion")){
		GameObject g_gr_flash_expl = Instantiate(gr_flash_expl,expl_spawn.position,expl_spawn.rotation) as GameObject;
		Destroy(active);
			active=g_gr_flash_expl;
		}
		
				if (GUI.Button(new Rect(20,140,200,20),"BOOM Explosion")){
		GameObject g_expl_w_t = Instantiate(expl_w_t,expl_spawn.position+new Vector3(0f,1f,0f),expl_spawn.rotation) as GameObject;
		Destroy(active);
			active=g_expl_w_t;
		}
		
		if (GUI.Button(new Rect(20,170,200,20),"Debris Explosion")){
		GameObject g_debris_explosion = Instantiate(debris_explosion,expl_spawn.position+new Vector3(0f,.5f,0f),expl_spawn.rotation) as GameObject;
		Destroy(active);
			active=g_debris_explosion;
		}
		
		
			if (GUI.Button(new Rect(20,200,200,20),"Big Explosion")){
		GameObject g_huge_expl = Instantiate(huge_expl,h_expl_spawn.position,h_expl_spawn.rotation) as GameObject;
		Destroy(active);
			active=g_huge_expl;
		}
	
			if (GUI.Button(new Rect(20,230,200,20),"Wave Explosion")){
		GameObject g_wave_expl = Instantiate(wave_expl,h_expl_spawn.position,h_expl_spawn.rotation) as GameObject;
		Destroy(active);
			active=g_wave_expl;
		}
		
				if (GUI.Button(new Rect(20,260,200,20),"Fireball")){
		GameObject g_fireball = Instantiate(fireball,expl_spawn.position,expl_spawn.rotation) as GameObject;
			g_fireball.AddComponent<RandomMovement>();
		Destroy(active);
			active=g_fireball;
		}
				if (GUI.Button(new Rect(20,290,200,20),"Dark Magic Fireball")){
		GameObject g_d_fireball = Instantiate(d_fireball,expl_spawn.position,expl_spawn.rotation) as GameObject;
			g_d_fireball.AddComponent<RandomMovement>();
		Destroy(active);
			active=g_d_fireball;
		}
		
					if (GUI.Button(new Rect(20,320,70,20),"Fire")){
		GameObject g_fire = Instantiate(fire,h_expl_spawn.position-new Vector3(0f,1.25f,0f),h_expl_spawn.rotation) as GameObject;
		Destroy(active);
			active=g_fire;
		}
		
						if (GUI.Button(new Rect(95,320,125,20),"Fire with smoke")){
		GameObject g_fire_sm = Instantiate(fire_sm,h_expl_spawn.position-new Vector3(0f,1.25f,0f),h_expl_spawn.rotation) as GameObject;
		Destroy(active);
			active=g_fire_sm;
		}
		
			if (GUI.Button(new Rect(20,350,105,20),"Attack Upgrade")){
		GameObject g_at_upr = Instantiate(attack_upgr,expl_spawn.position-new Vector3(0f,.8f,0f),expl_spawn.rotation) as GameObject;
		Destroy(active);
			active=g_at_upr;
		}
		
			if (GUI.Button(new Rect(130,350,90,20),"Broken Armor")){
		GameObject g_arm_br = Instantiate(armor_broken,expl_spawn.position-new Vector3(0f,.8f,0f),expl_spawn.rotation) as GameObject;
		Destroy(active);
			active=g_arm_br;
		}
	
			if (GUI.Button(new Rect(20,380,200,20),"Upgrade")){
		GameObject g_up = Instantiate(upgrade,expl_spawn.position-new Vector3(0f,.8f,0f),expl_spawn.rotation) as GameObject;
		Destroy(active);
			active=g_up;
		}
		
			if (GUI.Button(new Rect(20,410,200,20),"Lightning")){
		GameObject g_lightning = Instantiate(lightning,expl_spawn.position-new Vector3(0f,.8f,0f),expl_spawn.rotation) as GameObject;
		Destroy(active);
			active=g_lightning;
		}
		
	if (GUI.Button(new Rect(Screen.width-220,20,200,20),"Debris")){
		GameObject g_debris = Instantiate(debris,expl_spawn.position+new Vector3(0f,1f,0f),expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_debris;
		}
		
	if (GUI.Button(new Rect(Screen.width-220,50,200,20),"BOOM Sign")){
		GameObject g_expl_sign = Instantiate(expl_sign,expl_spawn.position+new Vector3(0f,1f,0f),expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_expl_sign;
		}	
		
	if (GUI.Button(new Rect(Screen.width-220,80,200,20),"Smoke")){
		GameObject g_smoke = Instantiate(smoke,expl_spawn.position,expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_smoke;
		}	
		
	if (GUI.Button(new Rect(Screen.width-220,110,200,20),"Flash 2")){
		GameObject g_flash_2 = Instantiate(flash_2,expl_spawn.position+new Vector3(0f,1f,0f),expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_flash_2;
		}	
		
	if (GUI.Button(new Rect(Screen.width-220,140,200,20),"Flash 3 (Hit)")){
		GameObject g_flash_3 = Instantiate(flash_3,expl_spawn.position+new Vector3(0f,1f,0f),expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_flash_3;
		}	
		
	if (GUI.Button(new Rect(Screen.width-220,170,200,20),"Flash with sign")){
		GameObject g_flash_3w = Instantiate(flash_3w,expl_spawn.position+new Vector3(0f,1f,0f),expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_flash_3w;
		}	
		
	if (GUI.Button(new Rect(Screen.width-220,200,200,20),"Wave")){
		GameObject g_wave = Instantiate(wave,h_expl_spawn.position,h_expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_wave;
		}	
		
	if (GUI.Button(new Rect(Screen.width-220,230,200,20),"Ground Hit")){
		GameObject g_gr_hit = Instantiate(gr_hit,expl_spawn.position,expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_gr_hit;
		}	
		
		if (GUI.Button(new Rect(Screen.width-220,260,200,20),"Stunt")){
		GameObject g_stunt = Instantiate(stunt,expl_spawn.position,expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_stunt;
		}	
		
		if (GUI.Button(new Rect(Screen.width-220,290,200,20),"Sleep")){
		GameObject g_sleep = Instantiate(sleep,expl_spawn.position,expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_sleep;
		}
		
			if (GUI.Button(new Rect(Screen.width-220,320,200,20),"Restore Health")){
		GameObject g_r_h = Instantiate(restore_health,expl_spawn.position-new Vector3(0f,.8f,0f),expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_r_h;
		}	
		
			if (GUI.Button(new Rect(Screen.width-220,350,200,20),"Blessing")){
		GameObject g_blessing = Instantiate(blessing,expl_spawn.position-new Vector3(0f,.8f,0f),expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_blessing;
		}	
		
			
					if (GUI.Button(new Rect(Screen.width-220,380,200,20),"Curse")){
		GameObject g_curse = Instantiate(curse,expl_spawn.position-new Vector3(0f,.8f,0f),expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_curse;
		}	
		
			if (GUI.Button(new Rect(Screen.width-220,410,200,20),"Curse_2(Toon)")){
		GameObject g_curse2 = Instantiate(curse_2,expl_spawn.position-new Vector3(0f,.8f,0f),expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_curse2;
		}
			if (GUI.Button(new Rect(Screen.width-220,440,200,20),"Poison")){
		GameObject g_poison = Instantiate(poison,expl_spawn.position-new Vector3(0f,.8f,0f),expl_spawn.rotation) as GameObject;
			Destroy(active);
			active= g_poison;
		}	
		
	}
}
