
using UnityEngine;

[CreateAssetMenu(fileName = "ShootStats", menuName = "ScriptableObjects/ShootStats")]
public class GunStats : ScriptableObject
{
    public int MagazineCount = 3; // How many times can this weapon reload
    public int MagazineCapacity = 3; // How many Ammo per reload 
    public float ReloadDelay = 1.0f; // How long the reload takes
    public float ShootDelay = 0.2f; // How long the shooting takes

    public float Power = 1;
    public float StartShootDelay = 0;
}