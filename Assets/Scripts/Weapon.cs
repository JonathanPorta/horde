using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon : System.Object {
  public string name = "Pistol";

  public GameObject prefab;
  public ParticleSystem particles;
  public Bullet bullet;

  public float damage = 50.0f; // Health points of damage caused by a single firing - indepenent of where it hits the other. (for now.)
  public float fireRate = 0.25f; // Seconds requird between each firing.
  public int burstRate = 1; // How much ammo is used each time the weapon is discharged.
  public int clipSize = 6; // of the current "clip" in the weapon.
  public int maxAmmo = 300; // Max ammo that can be held for this weapon.
  public float range = 150.0f; // Max range at which damage can be done by projectile.

  public int ammo = 14; // total ammo - ammo in clip.
  public int clipAmmo = 6; // the ammount of ammo in the current "clip"
}

[System.Serializable]
public class Bullet : System.Object {
  public GameObject prefab;
}