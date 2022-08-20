using ECommersionAPI.Application.Repositories;
using ECommersionAPI.Persistence.Contexts;

namespace ECommersionAPI.Persistence.Repositories
{
    public class FileWriteRepository : WriteRepository<ECommersionAPI.Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(ECommersionAPIDbContext context) : base(context)
        {
        }
    }
}
