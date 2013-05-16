using System;
using System.Collections;
using PassOne.Models;

namespace PassOne.Domain
{
    [Serializable]
    public class User : PassOneObject
    {
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Username { get;  set; }
        public string Password { get;  set; }
        public byte[] K { get; set; }
        public byte[] V { get; set; }

        //Constuctors
        public User()
        {
        }

        public User(string first, string last, string user, string pass)
        {
            CheckForMissingInformation(first, last, user, pass);
            FirstName = first;
            LastName = last;
            Username = user;
            Password = pass;
            K = Encryption.GenerateEncryptionKey();
            V = Encryption.GenerateEncryptionVector();
        }

        public User(int id, string first, string last, string user, string pass)
        {
            CheckForMissingInformation(first, last, user, pass);
            Id = id;
            FirstName = first;
            LastName = last;
            Username = user;
            Password = pass;
            K = Encryption.GenerateEncryptionKey();
            V = Encryption.GenerateEncryptionVector();
        }

        public User(int id, string first, string last, string user, string pass, byte[] k, byte[] v)
        {
            CheckForMissingInformation(first, last, user, pass);
            Id = id;
            FirstName = first;
            LastName = last;
            Username = user;
            Password = pass;
        }        

        public User(EntityUser entity)
        {
            Id = entity.Id;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Username = entity.Username;
            Password = entity.Password;
            K = entity.k;
            V = entity.v;
        }

        private void CheckForMissingInformation(string fn, string ln, string username, string password)
        {
            if (fn == string.Empty)
                throw new MissingInformationException("your first name");
            if (ln == string.Empty)
                throw new MissingInformationException("your last name");
            if (username == string.Empty)
                throw new MissingInformationException("a username");
            if (password == string.Empty)
                throw new MissingInformationException("a password");
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        protected bool Equals(User other)
        {
            return string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) && string.Equals(Username, other.Username) && string.Equals(Password, other.Password) && Equals(K, other.K) && Equals(V, other.V);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Username != null ? Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Password != null ? Password.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (K != null ? K.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (V != null ? V.GetHashCode() : 0);
                return hashCode;
            }
        }
        public override string ToString()
        {
            return "First name: " + FirstName +
                   "/nLast name: " + LastName +
                   "/nUsername: " + Username +
                   "/nPassword: " + Password;
        }
    }
}
