using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {

	public enum GunType{singleShot, beam, autoRifle};

	public class Gun : Unlock {

		public int clipSize;
		public int ammo;
		public float reloadTime;
		public GunType gunType;
		public float baseDamage;
		public bool singleKill;
		public int audio;

		public float fireRate;
		public float nextFire;

		public bool canFire;
		public bool reloading;

		public Gun(string name, int audio, int clipSize, float reloadTime, float fireRate, GunType gunType, float baseDamage, string description, int cost)
		{
			this.setName (name);
			this.audio = audio;
			this.setResearched (false);
			this.clipSize = clipSize;
			this.ammo = clipSize;
			this.gunType = gunType;
			this.baseDamage = baseDamage;
			this.reloadTime = reloadTime;
			this.fireRate = fireRate;
			this.description = description;
			this.cost = cost;
			canFire = true;
			reloading = false;
			nextFire = 0;
		}
	}

	/* Gun(
	 * 		name			: String
	 * 		audio
	 * 		clipsize		: int
	 * 		reload Time		: float
	 * 		fire rate		: float
	 * 		gun type		: GunType
	 * 		base damage		: float
	 * 		single kill		: bool
	 * 		description		: String
	 * ) */

	public static GameObject player;

	public AudioClip[] weaponAudio = new AudioClip[10];
	public AudioClip[] weaponReload = new AudioClip[10];

	public static int weaponNum;

	public static Gun[] playerWeapons = new Gun[11];
	public static Gun selectedWeapon;

	public static AudioSource gunSounds;

	void Start()
	{
		player = this.gameObject;
		gunSounds.clip = weaponAudio[selectedWeapon.audio];
	}

	void Awake()
	{
		playerWeapons[0].setResearched (true);
		playerWeapons [0].purchased = true;


		gunSounds = GameObject.Find ("Player").GetComponent<AudioSource> ();
	}

	public static void setFirstWepon(int i)
	{
		weaponNum = i;
		selectedWeapon = playerWeapons [weaponNum];
	}

#if UNITY_EDITOR
	void Update()
	{
		if (Input.GetButtonDown ("nextWeapon") && weaponNum < playerWeapons.Length - 1) {
			weaponNum++;
			selectedWeapon = playerWeapons [weaponNum];
		}

		if (Input.GetButton ("previousWeapon") && weaponNum > 0) {
			weaponNum--;
			selectedWeapon = playerWeapons [weaponNum];
		}
	}
#endif

	public static void createWeapons()
	{
		playerWeapons [00] = new Gun ("Blaster 3000X", 0, 8, 1.2f, 0.1f, GunType.singleShot, 75f, "The choice weapon of the Royal Space Fleet. It won't be much use in a war but it'll get you out of a jam.", 000);
		
		playerWeapons [01] = new Gun ("Carmack Rifle", 1, 6, 1.0f, 0.9f, GunType.singleShot, 125f, "The Carmack Rifle makes use of good ol' fashioned gun powder, it'll get the job done one way or another.", 100);
		
		playerWeapons [02] = new Gun ("M24 Assault Rifle", 2, 30, 2f, 0.15f, GunType.autoRifle, 40f, "The M24 Assault Rifle makes killing aliens fun and easy. Simply hold down the trigger and watch them die.", 175);
		
		playerWeapons [03] = new Gun ("Tesla Rifle", 3, 7, 1.5f, 1.5f, GunType.singleShot, 150f, "This weapon deals damage like nothing you've ever seen, unfortunatly it takes a while to actually shoot.", 250);
		
		playerWeapons [04] = new Gun ("Skull Crusher", 4, 25, 2.5f, 0.12f, GunType.autoRifle, 75f, "Created from what looks like smashing two guns together; the 'Skull Crusher' still manages to deliver deadly damage.", 375);
		playerWeapons [05] = new Gun ("Scar", 5, 10, 0.1f, 0.1f, GunType.singleShot, 225f, "The Scar Rifle is aptly named as it looks like it's survived several wars, but apparently it packs a punch.", 500);
		playerWeapons [06] = new Gun ("Eden", 6, 6, 2.5f, 0.2f, GunType.autoRifle, 175f, "'Powerful beyond measure...' is engraved on the side of this weapon, as you look at it it gives off a faint glow.", 650);
		
		playerWeapons [07] = new Gun ("DSX0-01", 7, 10, 0.1f, 0.3f, GunType.singleShot, 40f, "The DSX0-01 was a prototype weapon stolen from Rhea military, you don't know what it does but it looks badass.", 715);
		playerWeapons [08] = new Gun ("Avenger", 8, 28, 1.5f, 0.15f, GunType.autoRifle, 40f, "Choice weapon of the Reflex gang, the Avenger is known for it fast fire rate and high damage.", 765);
		
		playerWeapons [09] = new Gun ("Proton Rifle", 9, 4, 2.5f, 1.1f, GunType.singleShot, 40f, "At the forefront of weapon tech, the Proton Rifle allows you to kill enemies in one, however not very quickly.", 825);
		playerWeapons [10] = new Gun ("Dominator", 10, 100, 4f, 0.15f, GunType.autoRifle, 100f, "This fusion gun harness plasma energy to destroy your enemies, removing them from the battlefield.", 900);
	}

	public static void fire(Vector2 position)
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(position, Screen.dpi * 0.003f, 256);

		if(selectedWeapon.ammo > 0 && selectedWeapon.nextFire < Time.time && selectedWeapon.canFire)
		{
			gunSounds.clip = player.GetComponent<Weapons>().weaponAudio[selectedWeapon.audio];
			selectedWeapon.ammo--;
			selectedWeapon.canFire = false;
			gunSounds.Play ();
			selectedWeapon.nextFire = Time.time + selectedWeapon.fireRate;

			if(hits.Length > 0)
			{
				for(int i = 0; i < hits.Length; i++)
				{
					if(hits[i].tag == "Enemy")
					{
						hits[0].gameObject.GetComponent<EnemyHealth>().Damage(selectedWeapon.baseDamage);
						break;
					}
					else if (hits[i].tag == "Missile")
					{
						hits[i].gameObject.GetComponent<Missile>().shootDown();
						break;
					}

					if(selectedWeapon.singleKill) break;
				}
			}
		}
		else if(selectedWeapon.ammo == 0 && selectedWeapon.nextFire < Time.time && selectedWeapon.canFire)
		{
			selectedWeapon.canFire = false;
			selectedWeapon.nextFire = Time.time + selectedWeapon.reloadTime;

			gunSounds.clip = player.GetComponent<Weapons>().weaponReload[selectedWeapon.audio];
			gunSounds.Play ();

			reload();
		}

	}

	public static void reload()
	{
		selectedWeapon.reloading = true;
		selectedWeapon.canFire = false;
		selectedWeapon.ammo = selectedWeapon.clipSize;
		selectedWeapon.reloading = false;
	}
}

