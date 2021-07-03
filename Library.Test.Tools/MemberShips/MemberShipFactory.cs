using Library.Persistance.EF;
using Library.Persistance.EF.MemberShips;
using Library.Services.MemberShips;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Test.Tools.MemberShips
{
    public class MemberShipFactory
    {
        public static MemberShipAppService CreateService(EFDataContext context)
        {
            var unitOfWork = new EFUnitOfWorkRepository(context);
            var repository = new EFMemberShipRepository(context);
            return new MemberShipAppService(repository, unitOfWork);
        }
    }
}
