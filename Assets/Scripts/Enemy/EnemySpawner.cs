using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState{SPAWNING, WAITING, COUNTING};

    [SerializeField] private Wave[] waves;
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float waveCountDown = 0;

    private SpawnState state = SpawnState.COUNTING;
    private int currentWave;


    [SerializeField] private Transform[] spawners;
    [SerializeField] private List<CharacterStats> enemyList;
    
    private void Start()
    {
        waveCountDown = timeBetweenWaves;
        currentWave = 0;
    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if(!EnemiesAreDead())
                return;
            else
                CompleteWave();
        }

        if(waveCountDown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[currentWave]));
            }
        }
        else
            waveCountDown -= Time.deltaTime;
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        for(int i = 0; i < wave.enemiesAmount; i++)
        {
            EnemySpawn(wave.enemy);
            yield return new WaitForSeconds(wave.delay);
        }
        
        state = SpawnState.WAITING;

        yield break;
    }

    private void EnemySpawn(GameObject enemy)
    {
        int randomInt = Random.RandomRange(1, spawners.Length);
        Transform randomSpawner = spawners[randomInt];
        GameObject newEnemy = Instantiate(enemy, randomSpawner.position, randomSpawner.rotation);
        CharacterStats newEnemyStats = newEnemy.GetComponent<CharacterStats>();

        enemyList.Add(newEnemyStats);
    }

    private bool EnemiesAreDead()
    {
        int i = 0;
        foreach(CharacterStats enemy in enemyList)
        {
            if(enemy.IsDead())
                i++;
            else
                return false;
        }
        return true;
    }

    private void CompleteWave()
    {
        Debug.Log("Wave Done");
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if(currentWave + 1 > waves.Length - 1)
        {
            currentWave = 0;
            Debug.Log("Complete all waves");
        }
        else
        {
            currentWave++;
        }
    }
}
