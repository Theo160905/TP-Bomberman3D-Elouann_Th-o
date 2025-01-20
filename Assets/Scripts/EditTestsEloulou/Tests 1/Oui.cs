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

        // -- Act --


        // -- Assert --
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator OuiWithEnumeratorPasses()
    {
        // -- Arrange --
        GameObject chrono = new GameObject();
        GameChrono gameChrono = chrono.AddComponent<GameChrono>();


        float oldTime = gameChrono.Timer;
        float timeToWait = 2;

        // -- Act --

        yield return new WaitForSeconds(timeToWait);

        // -- Assert --
        Assert.That(gameChrono.Timer, Is.EqualTo(oldTime + timeToWait));
    }
}
