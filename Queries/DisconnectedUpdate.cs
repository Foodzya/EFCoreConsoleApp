using EcommerceStore.Core.Entities;
using EcommerceStore.Infrastucture.Persistence.Context;
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
                Id = 5,
                PhoneNumber = "917-243-9789",
                Email = "myoldemail@gmail.com",
                FirstName = "Alex",
                LastName = "Bordson",
                RoleId = 2
            };

            using (EcommerceContext context = new())
            {
                context.Users.Attach(user);

                user.Email = "mynewemail@gmail.com";

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