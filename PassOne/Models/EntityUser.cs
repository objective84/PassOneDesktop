using System;
using System.Collections.Generic;
using PassOne.Domain;

namespace PassOne.Models
{
    public partial class EntityUser
    {
        public EntityUser()
        {
            this.Credentials = new List<EntityCredential>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] k { get; set; }
        public byte[] v { get; set; }
        public virtual ICollection<EntityCredential> Credentials { get; set; }

        public EntityUser(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Password = user.Password;
            k = user.K;
            v = user.V;
        }
    }
}
