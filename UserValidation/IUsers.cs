using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.UserValidation
{
    public interface IUsers
    {
        int User_id();
        string Fname();
        string Lname();
        string Email();
        string DOB();
        string PhoneNumber();
        string Street();
        string Suburb();
        string City();
        string Country();
        string Password();
    }
}
