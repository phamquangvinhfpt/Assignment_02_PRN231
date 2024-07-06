using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eBookStoreWebAPI.Models
{
    public class LoginModel
    {
        [Required (ErrorMessage = "Email is empty!"), DataType(DataType.EmailAddress, ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required (ErrorMessage = "Password is empty!"), DataType(DataType.Password, ErrorMessage = "Invalid password")]
        public string Password { get; set; }
    }
}