using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.Common
{
    public class DefoultCommon : ICommon
    {
        public string GetCurrentUserName()
        {
            return "system";
        }
    }
}
