using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service_WCF.AdatbazisKezelese
{
    internal interface IDQL
    {
        List<Rekord> Select();
    }
}
