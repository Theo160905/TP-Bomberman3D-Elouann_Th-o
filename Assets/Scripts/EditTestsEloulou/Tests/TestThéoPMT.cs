using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestThéoPMT
{

    [UnityTest]
    public IEnumerator TestPOURTESTER()
    {
        // -- Arrange --

        GameObject player = new GameObject();
        PlayerUseBomb playerUseBomb = player.AddComponent<PlayerUseBomb>();

        playerUseBomb.usebomb = false;

        // -- Act --

        playerUseBomb.StartCoroutine(playerUseBomb.Wait());
        yield return new WaitForSeconds(1);

        // -- Assert -- 
        
        Assert.That(playerUseBomb.usebomb, Is.False);
    }
} 
