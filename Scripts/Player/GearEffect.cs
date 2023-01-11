using UnityEngine;
using System.Collections;

public class GearEffect : MonoBehaviour {

	string gearName;
	Animator anim;
	public bool followPlayerAnims = false;
	public Item theItem;
	static float originDash;

	void Start () {
		this.gearName = gameObject.name.Replace("(Clone)","");
		this.applyGearEffect();
	}

	void applyGearEffect() {
		if(this.gearName == "goldenCharm"){
			Player.baseSpeed += 0.2f;
		}
		if(this.gearName == "natureCharm"){
			Player.maxStamina += 100;
		}
		if(this.gearName == "bloodCharm"){
			Player.damage += 3;
		}
		if(this.gearName == "mysticCrown"){
			Player.baseSpeed += 0.2f;
			Player.damage += 2;
			Player.defense += 2;
			Player.staminaRegen += 1;
		}
		if(this.gearName == "titanBoots" || this.gearName == "hephaestusBoots" || this.gearName == "boreasBoots"){
			Player.dashSpeed += 2f;
		}
		if(this.gearName == "goldenSkull"){
			Player.knockBackForce -= 0.5f;
		}
		if(this.gearName == "titanShard"){
			Player.defense += 3;
		}
		if(this.gearName == "styxPenny"){
			Player.hasStyxPenny = true;
		}
		if(this.gearName == "boneCharm"){
			Player.staminaRegen += 1;
		}
		if(this.gearName == "scallopShell"){
			Player.defense += 2;
		}
		if(this.gearName == "ravenFeather"){
			Player.maxStamina += 100;
		}
		if(this.gearName == "talaria"){
			Player.baseSpeed += 0.3f;
		}
		if(this.gearName == "burningTorch"){
			Player.damage += 4;
		}
		if(this.gearName == "blackthornBranch"){
			GameObject.Find("Player").GetComponent<Player>().herbDropRate += 0.08f;
		}
		if(this.gearName == "moonstone"){
			Player.dashSpeed += 2.7f;
		}
		if(this.gearName == "ringOfHermes"){
			originDash = Player.dashSpeed;
			Player.dashSpeed = 0f;
		}
	}

	void OnDestroy() {
		if(this.gearName == "goldenCharm"){
			Player.baseSpeed -= 0.2f;
		}
		if(this.gearName == "natureCharm"){
			Player.maxStamina -= 100;
		}
		if(this.gearName == "bloodCharm"){
			Player.damage -= 3;
		}
		if(this.gearName == "mysticCrown"){
			Player.baseSpeed -= 0.2f;
			Player.damage -= 2;
			Player.defense -= 2;
			Player.staminaRegen -= 1;
		}
		if(this.gearName == "titanBoots" || this.gearName == "hephaestusBoots" || this.gearName == "boreasBoots"){
			Player.dashSpeed -= 2f;
		}
		if(this.gearName == "goldenSkull"){
			Player.knockBackForce += 0.5f;
		}
		if(this.gearName == "titanShard"){
			Player.defense -= 3;
		}
		if(this.gearName == "styxPenny"){
			Player.hasStyxPenny = false;
		}
		if(this.gearName == "boneCharm"){
			Player.staminaRegen -= 1;
		}
		if(this.gearName == "scallopShell"){
			Player.defense -= 2;
		}
		if(this.gearName == "ravenFeather"){
			Player.maxStamina -= 100;
		}
		if(this.gearName == "talaria"){
			Player.baseSpeed -= 0.3f;
		}
		if(this.gearName == "burningTorch"){
			Player.damage -= 4;
		}
		if(this.gearName == "blackthornBranch"){
			GameObject.Find("Player").GetComponent<Player>().herbDropRate -= 0.08f;
		}
		if(this.gearName == "moonstone"){
			Player.dashSpeed -= 2.7f;
		}
		if(this.gearName == "ringOfHermes"){
			Player.dashSpeed = originDash;
		}
	}

	void Update () {
	
		if(followPlayerAnims){
			anim = GetComponent<Animator> ();
			if(!Player.isAttacking){
				
				if(Player.direction == 1){anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", 1f);}
				else if(Player.direction == 2){anim.SetFloat("inputX", 1f);anim.SetFloat("inputY", 0f);
					//if(Player.movement_vector == new Vector2(-1f, 0f)){anim.SetBool("backwards", true);}
				}
				else if(Player.direction == 3){anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", -1f);}
				else if(Player.direction == 4){anim.SetFloat("inputX", -1f);anim.SetFloat("inputY", 0f);	
					//if(Player.movement_vector == new Vector2(1f, 0f)){anim.SetBool("backwards", true);}
				}
				
				if (Player.movement_vector != Vector2.zero) {
					anim.SetBool ("isWalking", true);
					
				} else {
					//anim.SetBool("backwards", false);
					anim.SetBool ("isWalking", false);
				}
			}
			//anim.SetBool ("isAttacking", Player.isAttacking);
			anim.SetBool ("isBlocking", Player.isBlocking);
		}

	}
}
