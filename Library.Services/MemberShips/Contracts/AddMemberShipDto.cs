﻿using System;

namespace Library.Services.MemberShips.Contracts
{
    public class AddMemberShipDto
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
    }
}
