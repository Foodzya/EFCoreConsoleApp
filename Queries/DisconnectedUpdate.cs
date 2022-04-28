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
                Id = 3,
                FirstName = "Jason",
                LastName = "Chaplin",
                Email = "chaplin.newemail4@armyspy.com",
                PhoneNumber = "949-733-7814",
                RoleId = 2
            };

            using (EcommerceContext context = new())
            {
                context.Users.Attach(user);

                user.Email = "jasonchaplin@armyspy.com";

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