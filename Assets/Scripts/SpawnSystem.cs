using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnSystem : MonoBehaviour
{
    //Floats
    private float maxTimeCounter;//actual vallue that counts down to 0
    private float nextSpawnTime;//for checking if if it is time for the next enemy to spawn
    //Floats
    
    //Int
    [SerializeField]
    private int spawnTime;
    [SerializeField]
    private int enemiesKilled;
    //Int
    [SerializeField]
    private LivingEntity[] enemy;
    //transforms
    [SerializeField]
    private Transform[] spawnpoints;
    //trandforms

    void Awake()
    {

    }

    void Update()
    {
        spawnCheck();
    }
    void spawnCheck()
    {

        //print(enemiesToSpawn);
        if(Time.time > nextSpawnTime)//checks if there are enemies left to spawn this wave and if it is time to spawn a enemy, if it executes it ajusts the amount of enemies that need to spawn, resets the timer and spawns a enemy
        {
            nextSpawnTime = Time.time + spawnTime;
            LivingEntity spawnedEnemy = Instantiate(enemy[Mathf.RoundToInt(Random.Range(0, enemy.Length))], spawnpoints[Mathf.RoundToInt(Random.Range(0, spawnpoints.Length))].position, Quaternion.identity) as LivingEntity;
            spawnedEnemy.OnDeath += OnEnemyDeath;//when ondeath is called, onenemydeath wil be called to
        }
    }

    void OnEnemyDeath()//decreases the int enemies alive and checks if next wave can start
    {
        enemiesKilled++;
    }
}