using System.Collections;
using System.Collections.Generic;
using GameConsole;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EchoTest
{
    [UnityTest]
    public IEnumerator EchoTestWithEnumeratorPasses()
    {
        GameObject consolePrefab = Resources.Load<GameObject>("Tests/Console");
        GameObject.Instantiate(consolePrefab);

        yield return null;

        UnityConsole console = GameObject.FindObjectOfType<UnityConsole>();

        UnityConsoleInput unityConsoleInput = GameObject.FindObjectOfType<UnityConsoleInput>();

        unityConsoleInput.consoleInput.ExecuteInput("echo test");
        Assert.AreEqual(console.console.consoleOutput.output[console.console.consoleOutput.output.Count - 1].message, "test");
    }
}
