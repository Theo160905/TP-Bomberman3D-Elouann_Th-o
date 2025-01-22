using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestsElouann
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestsElouannSimplePasses()
    {
        // -- Arrange --
        float test = 0f;

        // -- Act --
        test += 5;

        // -- Assert --
        Assert.That(test, Is.EqualTo(5));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestsElouannWithEnumeratorPasses()
    {
        // -- Arrange --
        GameObject chrono = new GameObject();
        GameChrono gameChrono = chrono.AddComponent<GameChrono>();
        
        float oldTime = gameChrono.Timer;
        float timeToWait = 2;

        // -- Act --

        gameChrono.ResumeTimer();
        yield return new WaitForSeconds(timeToWait);

        // -- Assert --
        Assert.That(gameChrono.Timer, Is.EqualTo(oldTime + timeToWait)); // n'est pas validé quand testé MAIS c'est un test, aie pitié par pitié
        yield return null;
    }
}
