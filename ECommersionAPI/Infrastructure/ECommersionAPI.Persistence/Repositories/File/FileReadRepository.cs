using ECommersionAPI.Application.Repositories;
using ECommersionAPI.Persistence.Contexts;

namespace ECommersionAPI.Persistence.Repositories
{
    public class FileReadRepository : ReadRepository<ECommersionAPI.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(ECommersionAPIDbContext context) : base(context)
        {
        }
    }
}
