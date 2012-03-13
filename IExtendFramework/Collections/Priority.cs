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
        [Enums.ReadableEnumAttribute("Highest Priority")]
        Highest = 5,
        [Enums.ReadableEnumAttribute("High Priority")]
        High = 4,
        [Enums.ReadableEnumAttribute("Normal Priority")]
        Normal = 3,
        [Enums.ReadableEnumAttribute("Low Priority")]
        Low = 2,
        [Enums.ReadableEnumAttribute("Lowest Priority")]
        Lowest = 1,
    }
}
