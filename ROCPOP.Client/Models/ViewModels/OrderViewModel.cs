using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel() {
            Products = new List<SessionCart>();
        }

        public List<SessionCart> Products{ get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Street address is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Zip code is required")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Email is required")]
        //[DataType(DataType.EmailAddress, ErrorMessage ="Please enter a valid Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email")]
        public string Email { get; set; }
        //[DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a phone number")]
        [Phone(ErrorMessage = "Please enter a valid Phone number")]
        public string Phone { get; set; }
        //public string FirstNameShipping { get; set; }
        //public string LastNameShipping { get; set; }
        //public string CompanyNameShipping { get; set; }
        public string StreetShipping { get; set; }
        public string CityShipping { get; set; }
        public string ZipCodeShipping { get; set; }
        public string CountryShipping { get; set; }
        public string OrderNotes { get; set; }
        public PhotoViewModel Photo { get; set; }
    }
}
