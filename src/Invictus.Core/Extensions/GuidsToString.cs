using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Extensions
{
    public static class GuidsToString
    {
        public static string ParseGuidsToString(this List<Guid> guids)
        {
            string listGuids = "";

            guids.ForEach(g => listGuids += g.ToString() + "/");

            listGuids = listGuids.Remove(listGuids.Length - 1);

            return listGuids;
        }
    }
}
