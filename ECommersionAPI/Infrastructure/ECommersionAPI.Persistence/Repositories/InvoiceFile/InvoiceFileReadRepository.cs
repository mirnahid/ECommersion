using ECommersionAPI.Application.Repositories;
using ECommersionAPI.Domain.Entities;
using ECommersionAPI.Persistence.Contexts;

namespace ECommersionAPI.Persistence.Repositories
{
    public class InvoiceFileReadRepository : ReadRepository<InvoiceFile>,IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(ECommersionAPIDbContext context) : base(context)
        {
        }
    }
}
