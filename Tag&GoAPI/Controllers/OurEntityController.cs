using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_GoAPI.Hubs;
using Tag_Go.DAL.Interfaces;
using Tag_GoAPI.DTOs.Forms;
using Tag_GoAPI.Tools;
using System.Security.Cryptography;
using Tag_Go.DAL.Entities;
//using System.Reflection.Metadata.Ecma335;

namespace Tag_GoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OurEntityController : ControllerBase
    {
    #nullable disable
        private readonly ApplicationDbContext _context;

        public OurEntityController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
