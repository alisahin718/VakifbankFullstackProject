using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = 1,
                    CreatedDate = DateTime.UtcNow,
                    Email = "sahin@sahin.com",
                    ImageUrl = "",
                    IsActive = true,
                    Name = "Ali",
                    Password = "Ali.1234",
                }
                );
        }
    }
}
