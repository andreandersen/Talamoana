using System;
using System.Collections.Generic;

namespace Talamoana.Domain.Core.Items.Crafting.Strategies
{
    public class RolledEventArgs
    {
        public Dictionary<Type, int> TotalRolled;
        public Item CurrentItem;
        public TimeSpan RunTime;

        public RolledEventArgs(Dictionary<Type, int> cost, Item currentItem, TimeSpan runTime)
        {
            TotalRolled = cost;
            CurrentItem = currentItem;
            RunTime = runTime;
        }
    }
}