using System;
using System.Linq;

namespace PassOne.Domain
{
    [Serializable]
    public class Credentials : PassOneObject
    {
        protected bool Equals(Credentials other)
        {
            return UserId == other.UserId && string.Equals(Website, other.Website) && string.Equals(Url, other.Url) &&
                   string.Equals(Username, other.Username) && string.Equals(Password, other.Password) &&
                   string.Equals(EmailAddress, other.EmailAddress);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = UserId;
                hashCode = (hashCode*397) ^ (Website != null ? Website.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Username != null ? Username.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Password != null ? Password.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (EmailAddress != null ? EmailAddress.GetHashCode() : 0);
                return hashCode;
            }
        }

        public int UserId { get; set; }
        public string Website { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }

        //public byte[] EncryptedWebsite { get; set; }
        //public byte[] EncryptedUrl { get; set; }
        //public byte[] EncryptedUsername { get; set; }
        //public byte[] EncryptedPassword { get; set; }
        //public byte[] EncryptedEmail { get; set; }

        //Constructors
        public Credentials()
        {
        }

        public Credentials(int id, string title)
        {
            Id = id;
            Website = title;
        }

        public Credentials(string title, string url, string username, string password, string email, int id = 0, int userId = 0 )
        {
            CheckForMissingInformation(title, username, password, email);
            Id = id;
            UserId = userId;
            Website = title;
            Url = url;
            Username = username;
            Password = password;
            EmailAddress = email;
        }

        public Credentials(Encryption encrypt, EncryptedCredentials encrypeted)
        {
            Id = encrypeted.Id;
            UserId = encrypeted.UserId;
            Website = encrypt.DecryptString(encrypeted.Website);
            Url = encrypt.DecryptString(encrypeted.Url);
            Username = encrypt.DecryptString(encrypeted.Username);
            Password = encrypt.DecryptString(encrypeted.Password);
            EmailAddress = encrypt.DecryptString(encrypeted.EmailAddress);
        }

        private void CheckForMissingInformation(string title, string username, string password, string email)
        {
            if (title == string.Empty)
                throw new MissingInformationException("a website name");
            if (username == string.Empty && email == string.Empty)
                throw new MissingInformationException("a username or email address");
            if (password == string.Empty)
                throw new MissingInformationException("a password");
        }

        ///// <summary>
        ///// Method to encrypt the contents of this credentials object, clears all string data when complete.
        ///// </summary>
        ///// <param name="encrypt">Encryption object containing key and vector values</param>
        //public static EncryptedCredentials Encrypt(Credentials creds, Encryption encrypt)
        //{
        //    var encrypted = new EncryptedCredentials
        //        {
        //            Website = encrypt.Encrypt(creds.Website),
        //            Url = encrypt.Encrypt(creds.Url),
        //            Username = encrypt.Encrypt(creds.Username),
        //            Password = encrypt.Encrypt(creds.Password),
        //            EmailAddress = encrypt.Encrypt(creds.EmailAddress)
        //        };
        //    return encrypted;
        //}

        ///// <summary>
        ///// Method to decrypt the contents of this Credentials object, clears all byte[] data when complete.
        ///// </summary>
        ///// <param name="encrypted"></param>
        ///// <param name="encrypt">Encryption object containing key and vectory values</param>
        //public void Decrypt(EncryptedCredentials encrypted, Encryption encrypt)
        //{
        //    Website = encrypt.Decrypt(encrypted.Website);
        //    Url = encrypt.Decrypt(encrypted.Url);
        //    Username = encrypt.Decrypt(encrypted.Username);
        //    Password = encrypt.Decrypt(encrypted.Password);
        //    EmailAddress = encrypt.Decrypt(encrypted.EmailAddress);
        //}

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Credentials) obj);
        }

        protected bool EncryptedEquals(byte[] thisByte, byte[] other)
        {
            if (thisByte == null || other == null)
                return thisByte == other;
            return !other.Where((t, i) => t != thisByte[i]).Any();
        }

        public override string ToString()
        {
            return "ID: " + Id +
                   "/nWebsite: " + Website +
                   "/nURL: " + Url +
                   "/nUsername: " + Username +
                   "/nPassword: " + Password +
                   "/nEmail Address: " + EmailAddress;
        }

        public Credentials Copy()
        {
            return new Credentials(Website, Url, Username, Password, EmailAddress, Id);
        }
    }
}
