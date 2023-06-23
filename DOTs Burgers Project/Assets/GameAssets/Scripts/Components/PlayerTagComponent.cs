using Unity.Entities;

namespace GameAssets.Scripts.Components
{
    public partial struct PlayerTagComponent : IComponentData
    {
        public int AssignedPlayerId;
    }
}