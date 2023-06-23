using System;

namespace GameAssets.Scripts.Temp
{
    /// <summary>
    /// Stores the data for an item, it's id, amount and other stuff
    /// </summary>
    [Serializable]
    public struct ItemData
    {
        public int ItemId;
        
        public static ItemData Null => new ItemData()
        {
            ItemId = -1
        };
        
    }
}