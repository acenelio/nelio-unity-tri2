using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Managers;
using NavGame.Core;

public class Level01Manager : LevelManager
{
    public Transform[] badSpawn;
    public GameObject badPrefab;
    public int badWaves = 3;
    public int monstersPerWave = 2;
    public float waitTimeFirstWave = 2f;
    public float waitTimeBetweenWaves = 4f;

    int totalCreep;

    protected override void Start()
    {
        base.Start();

        totalCreep = badWaves * monstersPerWave;

        if (onWaveUpdate != null)
        {
            onWaveUpdate(badWaves, 0);
        }
    }

    void OnCreepDied()
    {
        totalCreep--;

        if (totalCreep == 0)
        {
            EmitVictoryEvent();
        }
    }

    protected override IEnumerator SpawnBad()
    {
        float wait = waitTimeFirstWave;
        while (wait > 0)
        {
            if (onWaveCountdown != null)
            {
                onWaveCountdown(wait);
            }
            wait -= Time.deltaTime;
            yield return null;
        }
        for (int i = 0; i < badWaves; i++)
        {
            for (int j = 0; j < badSpawn.Length; j++)
            {
                for (int k = 0; k < monstersPerWave; k++)
                {
                    Vector3 offset = new Vector3(Random.Range(-0.1f, 0.1f), 0f, Random.Range(-0.1f, 0.1f));
                    
                    GameObject obj = Instantiate(badPrefab, badSpawn[j].position + offset, Quaternion.identity) as GameObject;
                    DamageableGameObject damageable = obj.GetComponent<DamageableGameObject>();
                    damageable.onDied += OnCreepDied;
                }
            }
            if (onWaveUpdate != null)
            {
                onWaveUpdate(badWaves, i + 1);
            }

            if (i < badWaves - 1)
            {
                wait = waitTimeBetweenWaves;
                while (wait > 0)
                {
                    if (onWaveCountdown != null)
                    {
                        onWaveCountdown(wait);
                    }
                    wait -= Time.deltaTime;
                    yield return null;
                }
            }
        }
        if (onWaveCountdown != null)
        {
            onWaveCountdown(0f);
        }
    }
}
