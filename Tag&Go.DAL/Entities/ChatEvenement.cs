using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag_Go.DAL.Entities
{
    public class ChatEvenement
    {
#nullable disable
        public int Chat_Id { get; set; }
        public string NewMessage { get; set; }
        public string Author { get; set; }
        public DateTime SendingDate { get; set; }
        public int NEvenement_Id { get; set; }
        public bool Active { get; set; }
    }
}
