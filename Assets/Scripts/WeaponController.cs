using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    private bool firing = false;
    private float lastFire = 0.0f;
    private float nextFire = 0.0f;


    //public void Fire(Vector3 direction){
    //	if(firing || Time.time < nextFire){ return ; } // No double firing and respect the fire rate.
    //	
    //	firing = true;
    //	lastFire = Time.time;
    //	nextFire = Time.time + fireRate;

    //	RaycastHit hit;

    //	Vector3 laserEndPoint;

    // Cast a ray as far as the weapon is effective and see if we hit something
    //	if(Physics.Raycast(transform.position, direction, out hit, effectiveRange)){
    //		GameObject recipient = hit.transform.gameObject;
    //		laserEndPoint = recipient.transform.position;

    //TODO: Move damage into the laserbolt script
    //		HitpointController hitpointController = recipient.GetComponent<HitpointController>();
    //		if(hitpointController != null){
    //			float damage = ComputeDamage(recipient.transform.position);
    //			hitpointController.Damage(damage);
    //		}
    //		else {
    //			Debug.Log("GameObject did not have a HitpointController. Not applying damages.");
    //		}
    //	}
    //	else { // We still need a point to draw our laser, even if we aren't hitting anything.
    //			Ray ray = new Ray(transform.position, direction);
    //		laserEndPoint = ray.GetPoint(effectiveRange);
    //	}
    //	ShowFiringEffects(laserEndPoint);
    //}

  public void Reload(Weapon weapon) {
    if(weapon.ammo == 0) return; // Sry, can't help you...
    int ammoNeeded = weapon.clipSize - weapon.clipAmmo;
    if(ammoNeeded >= weapon.ammo) {
      weapon.clipAmmo = weapon.ammo;
      weapon.ammo = 0;
    }
    else {
      weapon.clipAmmo = weapon.clipSize;
      weapon.ammo = weapon.ammo - ammoNeeded;
    }
  }

  public void Shoot(Weapon weapon, Vector3 direction, PlayerController.KillDelegate kd) {
    //Debug.Log("Before check fire rate.");
    if(firing || Time.time < nextFire) { return; } // No double firing and respect the fire rate.
    //Debug.Log("Before ammo.");
    if(weapon.clipAmmo <= 0) { return; } // Can't shoot if we're out of ammo. click-click

    // Make sure we aren't firing too much.
    //firing = true;
    lastFire = Time.time;
    nextFire = Time.time + weapon.fireRate;

    // Deplete the ammo
    weapon.clipAmmo = weapon.clipAmmo - weapon.burstRate;
    // If don't have enough in the current clip to shoot all burst rounds, then only shoot until the clip is empty. No free reloads here!
    if(weapon.clipAmmo < 0)
      weapon.clipAmmo = 0;

    RaycastHit hit;
    Vector3 laserEndPoint;

    // Cast a ray as far as the weapon is effective and see if we hit something
    if(Physics.Raycast(transform.position, direction, out hit, weapon.range)) {
      GameObject recipient = hit.transform.gameObject;
      if(recipient.CompareTag("Enemy")) {
        EnemyController ec = recipient.GetComponent<EnemyController>() as EnemyController;
        ec.Hit(weapon);

        // Check for killage
        if(ec.state.health <= 0.0f) {
          Debug.Log("Dead!");
          kd(ec);
        }
        Debug.Log("Hit!");
      }
      //laserEndPoint = recipient.transform.position;
    }


    //float damage = ComputeDamage(target.transform.position);

    // Damage the target
    //target.GetComponent<HitpointController>().Damage(damage);
    //Debug.Log("Weapon Fired!");
    //Debug.Log(weapon.ammo);

    // ShowFiringEffects();
  }

  //private float ComputeDamage(Vector3 target){
  //TODO: Move damage into the laserbolt script
  //float distance = Vector3.Distance(transform.position, target);
  //if(distance > effectiveRange) // Weapon is not effective past its effective range
  //	return 0.0f;
  //float effectiveness = Mathf.Clamp01((effectiveRange - distance) / effectiveRange);
  //float damage = minimumDamage + scaledDamage * effectiveness;
  //return damage;
  //}

  //    private void ShowFiringEffects() {
  //        GameObject bolt = Instantiate(laserBoltPrefab, transform.position, transform.rotation) as GameObject;
  //        bolt.GetComponentInChildren<Renderer>().material = laserBoltMaterial;
  //        bolt.GetComponentInChildren<LaserBolt>().effectiveRange = effectiveRange;
  //
  //        // Flash the light
  //        laserShotLight.intensity = flashIntensity;
  //
  //        // Play the gun shot clip at the position of the muzzle flare.
  //        AudioSource.PlayClipAtPoint(shotClip, laserShotLight.transform.position);
  //    }
}