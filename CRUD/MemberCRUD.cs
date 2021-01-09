using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.CRUD
{
    public class MemberCRUD : IBaseCrud
    {
        public int Create()
        {
            return 0;
        }

        public MemberViewModel Read(int memberId)
        {
            // get member by member id
            return new MemberViewModel();
        }

        public int Update()
        {
            return 0;
        }

        public int Destroy()
        {
            return 0;
        }
    }
}