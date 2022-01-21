using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? fromDate, DateTime? untilDate)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (fromDate.HasValue)
            {
                result = result.Where(x => x.Date >= fromDate.Value);
            }

            if (untilDate.HasValue)
            {
                result = result.Where(x => x.Date <= untilDate.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? fromDate, DateTime? untilDate)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (fromDate.HasValue)
            {
                result = result.Where(x => x.Date >= fromDate.Value);
            }

            if (untilDate.HasValue)
            {
                result = result.Where(x => x.Date <= untilDate.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}
