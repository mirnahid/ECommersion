using ECommersionAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommersionAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
