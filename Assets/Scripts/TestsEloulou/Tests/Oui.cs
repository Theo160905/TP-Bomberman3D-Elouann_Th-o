using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Oui
{
    // A Test behaves as an ordinary method
    [Test]
    public void OuiSimplePasses()
    {
        // -- Arrange --
        int spawnCount = SpawnerBomb.Instance.spawnCount;
        List<GameObject> bombsList = SpawnerBomb.Instance.OnMapBombs;

        // -- Act --
        

        // -- Assert --
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator OuiWithEnumeratorPasses()
    {
        // -- Arrange --
        // -- Act --
        // -- Assert --
        yield return null;
    }
}
