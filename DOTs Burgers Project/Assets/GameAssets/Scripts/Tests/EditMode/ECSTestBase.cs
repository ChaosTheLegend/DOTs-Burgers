using NUnit.Framework;
using Unity.Entities;

namespace GameAssets.Scripts.Tests.EditMode
{
    public abstract class ECSTestBase
    {
        protected World World { get; private set; }
        protected EntityManager EntityManager { get; private set; }
 
        [SetUp]
        public void SetUpBase()
        {
            World = new World("Test World");
            EntityManager = World.EntityManager;
            var allSystems = DefaultWorldInitialization.GetAllSystems(WorldSystemFilterFlags.Default, false);
            DefaultWorldInitialization.AddSystemsToRootLevelSystemGroups(World, allSystems);
        }
    
        [TearDown]
        public void TearDownBase()
        {
            World.Dispose();
        }
    }
}