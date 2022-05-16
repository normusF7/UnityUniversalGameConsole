using System.Collections;
using System.Collections.Generic;
using GameConsole;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WrongCommandTest
{
    [UnityTest]
    public IEnumerator WrongCommandTestWithEnumeratorPasses()
    {
        yield return null;

        //GameObject consolePrefab = Resources.Load<GameObject>("Tests/Console");
        //GameObject.Instantiate(consolePrefab);

        UnityConsole console = GameObject.FindObjectOfType<UnityConsole>();

        UnityConsoleInput unityConsoleInput = GameObject.FindObjectOfType<UnityConsoleInput>();

        string commandToExecute = "badcmd";
        unityConsoleInput.consoleInput.ExecuteInput(commandToExecute);
        Assert.AreEqual(console.console.consoleOutput.output[console.console.consoleOutput.output.Count - 1].message, string.Format(Console.ERR_CMD_NOT_RECOGNIZED, commandToExecute));
    }
}
