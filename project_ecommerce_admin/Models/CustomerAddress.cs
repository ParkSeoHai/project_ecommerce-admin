namespace project_ecommerce_admin.Models
{
    public class CustomerAddress
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDefault { get; set; }

        // Foreign key table Customer
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
