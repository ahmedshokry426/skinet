using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> spec) 
        {
            var query = inputQuery;

            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // p=>p.ProdcutTypeId == id    
            }

            /*return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync(); ده بيعمل نفس الي تحت بظبط*/
            //Include product type and brand thats why we used "Aggregate".
            query = spec.Includes.Aggregate(query,
            (current, include)=> current.Include(include));

            return query;
        }  
    }
}