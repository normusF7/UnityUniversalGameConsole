using System;
using System.Collections.Generic;
using System.Linq;

namespace GameConsole
{
    public static class CommandsProvider
    {
        public static List<BaseCommand> commands;

       [UnityEditor.Callbacks.DidReloadScripts]
        private static void GatherCommands()
        {
            commands?.Clear();
            commands = typeof(BaseCommand)
    .Assembly.GetTypes()
    .Where(t => t.IsSubclassOf(typeof(BaseCommand)) && !t.IsAbstract)
    .Select(t => (BaseCommand)Activator.CreateInstance(t)).ToList();
        }
    }
}
