using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Application.Users.Dtos;

public class AddUserDto
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Mobile { get; set; }
    
    public string? NationalCode { get; set; }

}
