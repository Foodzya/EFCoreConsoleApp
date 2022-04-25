using EcommerceStore.Data.Context;
using EcommerceStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EcommerceStore.Queries
{
    public class DisconnectedUpdate
    {
        public void UpdateUserExplicitTracking()
        {
            var user = new User()
            {
<<<<<<< HEAD
                Id = 5,
                PhoneNumber = "917-243-9789",
                Email = "myoldemail@gmail.com",
                FirstName = "Alex",
                LastName = "Bordson",
=======
                Id = 3,
                FirstName = "Jason",
                LastName = "Chaplin",
                Email = "chaplin.newemail4@armyspy.com",
                PhoneNumber = "949-733-7814",
>>>>>>> 0c7a5b8bbdd0eeff4e60568aac8c00d2b622aa12
                RoleId = 2
            };

            using (EcommerceContext context = new())
            {
                context.Users.Attach(user);

<<<<<<< HEAD
                user.Email = "mynewemail@gmail.com";
=======
                user.Email = "jasonchaplin@armyspy.com";
>>>>>>> 0c7a5b8bbdd0eeff4e60568aac8c00d2b622aa12

                context.SaveChanges();
            }
        }

        public void UpdateUserWithEntityState()
        {
            User user = null;

            using (EcommerceContext context = new())
            {
                user = context.Users.First(u => u.Id == 2);
            }

            using (EcommerceContext anotherContext = new())
            {
                Console.WriteLine(anotherContext.ChangeTracker.DebugView.LongView);

                user.Email = "mynewemailchaplin@gmail.com";

                anotherContext.Entry(user).State = EntityState.Modified;

                Console.WriteLine(anotherContext.ChangeTracker.DebugView.LongView);

                anotherContext.SaveChanges();

                Console.WriteLine(anotherContext.ChangeTracker.DebugView.LongView);
            }
        }
    }
}