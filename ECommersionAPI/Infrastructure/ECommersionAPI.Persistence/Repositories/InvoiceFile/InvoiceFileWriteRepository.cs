using ECommersionAPI.Application.Repositories;
using ECommersionAPI.Domain.Entities;
using ECommersionAPI.Persistence.Contexts;

namespace ECommersionAPI.Persistence.Repositories
{
    public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>,IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(ECommersionAPIDbContext context) : base(context)
        {
        }
    }
}
