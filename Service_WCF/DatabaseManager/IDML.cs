using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service_WCF.AdatbazisKezelese
{
    internal interface IDML
    {
        int Insert(Rekord rekord);

        int Update(Rekord rekord);

        int Delete(int id);
    }
}