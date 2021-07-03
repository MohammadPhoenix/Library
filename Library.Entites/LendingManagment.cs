using System;

namespace Library.Entites
{
    public class LendingManagment
    {
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        public MemberShip Member { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime AuthorizedDeliveryDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
