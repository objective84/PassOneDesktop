using System;
using System.Collections.Generic;
using PassOne.Domain;

namespace PassOne.Models
{
    public partial class EntityCredential
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Website { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual EntityUser User { get; set; }

        public EntityCredential(){}

        public EntityCredential(Encryption encrypt, Credentials creds)
        {
            Id = creds.Id;
            UserId = creds.UserId;
            Website = encrypt.EncryptToString(creds.Website);
            Url = encrypt.EncryptToString(creds.Url);
            Username = encrypt.EncryptToString(creds.Username);
            Password = encrypt.EncryptToString(creds.Password);
            Email = encrypt.EncryptToString(creds.EmailAddress);
        }
    }
}
