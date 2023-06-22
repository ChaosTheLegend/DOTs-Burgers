using Rewired;
using Unity.Entities;
using UnityEngine;

namespace GameAssets.Scripts.Aspects
{
    public partial struct PlayerTagComponent : IComponentData
    {
        public int AssignedPlayerId;
    }
}