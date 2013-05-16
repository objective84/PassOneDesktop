using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PassOne.Domain;
using PassOne.Models;

namespace PassOne.Service
{
    public class EntityCredentialsImplementation : IPassOneDataSvc
    {
        public PassOneObject RetreiveById(int id)
        {
            var db = new PassOneContext();
            var query = from u in db.Credentials select u;
            var creds = query.ToList();


            var selected = new EntityCredential();
            foreach (var cred in query.Where(cred => cred.Id == id))
                selected = cred;

            var userQuery = from u in db.Users select u;
            var user = userQuery.ToList().FirstOrDefault(user1 => user1.Id == selected.UserId);
            return ConvertToDomainObject(new User(user), selected);
        }

        public void Create(PassOneObject obj)
        {
            obj.Id = GetNextIdValue();
            var creds = (Credentials) obj;
            using (var db = new PassOneContext())
            {
                var userQuery = from u in db.Users select u;
                var user = userQuery.ToList().FirstOrDefault(user1 => user1.Id == creds.UserId);
                var entity = new EntityCredential(new Encryption(user.k, user.v), creds) {User = user};
                db.Credentials.Add(entity);
                db.SaveChanges();
            }
        }

        public void Delete(PassOneObject obj)
        {
            using (var db = new PassOneContext())
            {
                var query = from c in db.Credentials select c;
                var creds = query.ToList().FirstOrDefault(creds1 => creds1.Id == obj.Id);
                db.Credentials.Remove(creds);
                db.SaveChanges();
            }
        }

        public void Edit(PassOneObject obj)
        {
            using (var db = new PassOneContext())
            {
                var credsQuery = from u in db.Credentials select u;
                var creds = credsQuery.ToList().FirstOrDefault(user1 => user1.Id == obj.Id);
                var userQuery = from u in db.Users select u;
                var user = userQuery.ToList().FirstOrDefault(user1 => user1.Id == creds.UserId);
                db.Credentials.Remove(creds);
                db.Credentials.Add(new EntityCredential(new Encryption(user.k, user.v), (Credentials) obj));
                db.SaveChanges();
            }
        }

        public int GetNextIdValue()
        {
            var context = new PassOneContext();
            var query = from u in context.Credentials select u;
            var users = query.ToList();

            return users.Select(user => user.Id).Concat(new[] { 0 }).Max() + 1;
        }

        public Dictionary<string, int> GetCredentialsList(int userId)
        {
            var list = new Dictionary<string, int>();
            using (var db = new PassOneContext())
            {
                var userQuery = from u in db.Users select u;
                var user = userQuery.ToList().FirstOrDefault(user1 => user1.Id == userId);
                var query = from c in db.Credentials select c;
                foreach (var credential in query.ToList())
                {
                    if (credential.UserId == userId)
                    {
                        var creds = ConvertToDomainObject(new User(user), credential);
                        list.Add(creds.Website, credential.Id);
                    }
                }
            }
            return list;
        }

        private Credentials ConvertToDomainObject(User user, EntityCredential entity)
        {
            var encrypted = new EncryptedCredentials()
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    Website = entity.Website,
                    Url = entity.Url,
                    Username = entity.Username,
                    EmailAddress = entity.Email,
                    Password = entity.Password
                };
            return new Credentials(new Encryption(user.K, user.V), encrypted);
        }
    }
}