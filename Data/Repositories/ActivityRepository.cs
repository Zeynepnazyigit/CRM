using Core.Abstracts.IRepositories;
using Core.Abstracts.IRepositories;
using Core.Concretes.Entities;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Utilities.Generics;

namespace Data.Repositories
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(ApplicationContext context) : base(context)
        {
        }

        public ActivityRepository(DbContext context) : base(context)
        {
        }
    }
}