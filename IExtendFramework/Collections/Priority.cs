/*
 * User: elijah
 * Date: 1/27/2012
 * Time: 5:53 PM
 */
using System;

namespace IExtendFramework.Collections
{
    public enum Priority
    {
        [Enums.EnumString("Highest Priority")]
        Highest = 5,
        [Enums.EnumString("High Priority")]
        High = 4,
        [Enums.EnumString("Normal Priority")]
        Normal = 3,
        [Enums.EnumString("Low Priority")]
        Low = 2,
        [Enums.EnumString("Lowest Priority")]
        Lowest = 1,
    }
}
