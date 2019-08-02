using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour 
{ 
    public GameObject [] powerUps; 
    public Vector3 powerUpValues; 
    public int powerUpCount; 
    public float powerUpSpawnWait; 
    public float powerUpStartWait;
    public float powerUpWaveWait; 
    public bool gameOver;

    public MeshCollider invincible;
    private PlayerController playerController;
 void Start ()
 {
     gameOver = false;
     StartCoroutine (PowerUpWaves());
     invincible = GetComponent <MeshCollider> ();
     PlayerController playerController = GetComponent<PlayerController>();
 }
 void Update ()
 {
     
 }
 void OnTriggerEnter (Collider other)
 {
     if (other.tag == "Gem")
     {
         StartCoroutine (PowerUpWearOff(5f));
     }
     if (other.tag == "PowerUp2")
     {
         playerController.fireRate = 0.03f;
     }

     
 }
 IEnumerator PowerUpWaves ()
 {
     yield return new WaitForSeconds (powerUpStartWait);
     while (true) 
     {
         for (int i = 0; i < powerUpCount; i++) 
         {
             GameObject powerUp = powerUps [Random.Range (0, powerUps.Length)];
             Vector3 powerUpPosition = new Vector3 (Random.Range (-powerUpValues.x, powerUpValues.x), powerUpValues.y, powerUpValues.z);
             Quaternion powerUpRotation = Quaternion.identity;
             Instantiate (powerUp, powerUpPosition, powerUpRotation);
             yield return new WaitForSeconds (powerUpSpawnWait);
         }
         yield return new WaitForSeconds (powerUpWaveWait);
        
     }
 }
 IEnumerator PowerUpWearOff(float waitTime)
 {
     invincible.enabled = false;
     yield return new WaitForSeconds(waitTime);
     invincible.enabled = true;
 }
 
    
}