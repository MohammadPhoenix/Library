using Library.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.LendingManagments.Contracts
{
    public interface LendingManagmentRepository
    {
        void Add(LendingManagment lendingManagmen);
        LendingManagment Find(int lendingManagmentId);
    }
}
