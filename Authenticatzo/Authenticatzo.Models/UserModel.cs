/// Mohamed Ali NOUIRA

using System;

namespace Authenticatzo.Models
{
    public class UserModel
    {
        
        public Guid Id { get; set; }
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
       
        public string EmailId { get; set; }
       
       public string token { get; set; }
    }
}
