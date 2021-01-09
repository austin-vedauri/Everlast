using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast
{
    public interface IBaseCrud
    {
        int Create();
        int Update();
        int Destroy();
    }
}