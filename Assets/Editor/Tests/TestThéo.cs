using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.CodeEditor;

public class TestThéo
{
    [Test]
    public void OnSpawnBomb_DoesNotReturnNegativePosition()
    {
        // -- Arrange --
        GameObject spawnerObject = new GameObject("SpawnerBomb");
        SpawnerBomb spawner = spawnerObject.AddComponent<SpawnerBomb>();

        GameObject poolBombObject = new GameObject("ObjectPoolBomb");
        ObjectPoolBomb poolBomb = poolBombObject.AddComponent<ObjectPoolBomb>();

        GameObject bombObject = new GameObject("Bomb");
        bombObject.AddComponent<Bomb>();
        bombObject.AddComponent<Collider>();

        poolBomb.objectPrefab = bombObject;
        poolBomb.poolSize = 6;

        int count = spawner.spawnCount;

        // -- Act -- 
        poolBomb.Test();

        for (int i = 0; i < 5; i++)
        {
            count--;
            poolBomb.ReturnObject(bombObject);
        }

        // -- Assert -- 
        Assert.That(count, Is.GreaterThan(0));
    }
}