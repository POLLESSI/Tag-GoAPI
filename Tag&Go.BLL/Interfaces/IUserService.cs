using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_Go.BLL.Interfaces
{
    public interface IUserService
    {
    #nullable disable
        Task<NUser> RegisterAsync(UserRegisterDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
    }
}
