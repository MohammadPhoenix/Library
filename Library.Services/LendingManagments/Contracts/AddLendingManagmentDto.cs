using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.LendingManagments.Contracts
{
    public class AddLendingManagmentDto
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime AuthorizedDeliveryDate { get; set; }
    }
}
