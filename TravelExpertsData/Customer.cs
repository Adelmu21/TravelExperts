using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsData
{
    [Index(nameof(AgentId), Name = "EmployeesCustomers")]
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            CreditCards = new HashSet<CreditCard>();
            CustomersRewards = new HashSet<CustomersReward>();
        }

        [Key]

        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(25)]
        public string CustFirstName { get; set; } = null!;

        [Required(ErrorMessage = "Please enter last name")]
        [StringLength(25)]
        public string CustLastName { get; set; } = null!;

        [Required(ErrorMessage = "Please enter address")]
        [StringLength(75)]
        public string CustAddress { get; set; } = null!;

        [Required(ErrorMessage = "Please enter city")]
        [StringLength(50)]
        public string CustCity { get; set; } = null!;

        [Required(ErrorMessage = "Please enter province")]
        [StringLength(2)]
        public string CustProv { get; set; } = null!;

        [Required(ErrorMessage = "Please enter postal code")]
        [RegularExpression(@"^[a-zA-Z]\d[a-zA-Z]\s?\d[a-zA-Z]\d$", ErrorMessage = "Postal code should be in the format A0A 0A0")]
        [StringLength(7)]
        public string CustPostal { get; set; } = null!;

        [Required(ErrorMessage = "Please enter country")]
        [StringLength(25)]
        public string? CustCountry { get; set; }

        [Required(ErrorMessage = "Please enter home phone number")]
        [StringLength(20)]
        public string? CustHomePhone { get; set; }

        [Required(ErrorMessage = "Please enter cell phone number")]
        [StringLength(20)]
        public string CustBusPhone { get; set; } = null!;

        [Required(ErrorMessage = "Please enter email address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(50)]
        public string CustEmail { get; set; } = null!;
        public int? AgentId { get; set; }

        [Required(ErrorMessage = "Please enter username")]
        [StringLength(30)]
        [Unicode(false)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength(30,
            ErrorMessage = "Please limit your password to 30 characters.")]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters.")]
        [Unicode(false)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [Display(Name = "Confirm Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [ForeignKey(nameof(AgentId))]
        [InverseProperty("Customers")]
        public virtual Agent? Agent { get; set; }
        [InverseProperty(nameof(Booking.Customer))]
        public virtual ICollection<Booking> Bookings { get; set; }
        [InverseProperty(nameof(CreditCard.Customer))]
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        [InverseProperty(nameof(CustomersReward.Customer))]
        public virtual ICollection<CustomersReward> CustomersRewards { get; set; }
    }
}
