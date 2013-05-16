using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PassOne.Domain;
using PassOne.Models;


namespace PassOne.Service
{
    public class EntityUserImplementation : IPassOneDataSvc
    {
        public PassOneObject RetreiveById(int id)
        {
            var context = new PassOneContext();
            var query = from u in context.Users select u;
            var users = query.ToList();

            var selected = users.FirstOrDefault(user => user.Id == id);
            return new User(selected);
        }

        public void Create(PassOneObject obj)
        {
            obj.Id = GetNextIdValue();
            using (var db = new PassOneContext())
            {
                db.Users.Add(new EntityUser((User)obj));
                db.SaveChanges();
            }
        }

        public void Delete(PassOneObject obj)
        {
            using (var db = new PassOneContext())
            {
                var userQuery = from u in db.Users select u;
                var user = userQuery.ToList().FirstOrDefault(user1 => user1.Id == obj.Id);
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public void Edit(PassOneObject obj)
        {
            using (var db = new PassOneContext())
            {
                var userQuery = from u in db.Users select u;
                var user = userQuery.ToList().FirstOrDefault(user1 => user1.Id == obj.Id);
                userQuery.ToList().Remove(user);
                userQuery.ToList().Add(new EntityUser((User) obj));
                db.SaveChanges();
            }
        }

        public int GetNextIdValue()
        {
            var context = new PassOneContext();
            var query = from u in context.Users select u;
            var users = query.ToList();

            return users.Select(user => user.Id).Concat(new[] { 0 }).Max() + 1;
        }

        public User Authenticate(string username, string password)
        {
            EntityUser user;
            using (var db = new PassOneContext())
            {
                var query = from u in db.Users select u;
                user = query.ToList().FirstOrDefault(user1 => user1.Username == username && user1.Password == password);
            }
            if (user == null)
                throw new InvalidLoginException();

            return new User(user);
        }
    }
}