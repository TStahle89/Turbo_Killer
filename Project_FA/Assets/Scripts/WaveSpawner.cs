using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[System.Serializable]

public class Wave
{
    public string waveName;
    public int numberOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;

}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public Animator myAnimator;
    public Text waveName;

    private Wave _currentWave;
    private int _currentWaveNumber;
    private float _nextSpawnTime;
    private bool _canSpawn = true;
    private bool _canAnimate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentWave = waves[_currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("FlyingEnemy");
        if (totalEnemies.Length == 0 && _currentWaveNumber+1 != waves.Length && _canAnimate)
        {
            waveName.text = waves[_currentWaveNumber + 1].waveName;
            myAnimator.SetTrigger("WaveComplete");
            _canAnimate = false;
        }
    }

    void SpawnNextWave()
    {
        _currentWaveNumber++;
        _canSpawn = true;
    }

    void SpawnWave()
    {
        if (_canSpawn && _nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = _currentWave.typeOfEnemies[Random.Range(0, _currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            _currentWave.numberOfEnemies--;
            _nextSpawnTime = Time.time + _currentWave.spawnInterval;
            if (_currentWave.numberOfEnemies == 0)
            {
                _canSpawn = false;
                _canAnimate = true;
            }
        }
    }
    
}
