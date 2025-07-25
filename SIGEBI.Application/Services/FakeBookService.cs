using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Application.Contracts.Services;

namespace SIGEBI.Application.Services
{
    public  class FakeBookService : IBookService
    {
        public Task<bool> IsBookAvailableAsync(int bookId)
        {
            return Task.FromResult(true);
        }
    }
}
