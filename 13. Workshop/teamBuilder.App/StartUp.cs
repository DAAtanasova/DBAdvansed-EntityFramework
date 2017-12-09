using System;
using Microsoft.EntityFrameworkCore;
using TeamBuilder.App.Core;
using TeamBuilder.Data;

namespace TeamBuilder.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            //ResetDatabase();

            Engine engine = new Engine(new CommandDispatcher());
            engine.Run();
        }

        private static void ResetDatabase()
        {
            using(var db = new TeamBuilderContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();
            }
        }
    }
}
