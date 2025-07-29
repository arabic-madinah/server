using Microsoft.EntityFrameworkCore;
using MyArabic.WebApi.Models;

namespace MyArabic.WebApi.DataAccess;

public class ReOrderEntityRepository(AppDbContext context)
{
    /// <summary>
    /// Performs necessary logic to re-order an entity.
    /// </summary>
    /// <param name="newOrder">new order value to set</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<int> ReOrderEntityAsync<T>(int? newOrder, CancellationToken cancellationToken) where T : class, ISortable
    {
        var order = newOrder ?? 0;
        if (newOrder.HasValue)
            // shift existing records to make space for the new entity
            await context.Set<T>()
                .Where(x => x.Order >= newOrder)
                .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Order, x => x.Order + 1), cancellationToken);
        else
            // otherwise, we append the new entity at the end
            order = (await context
                .Set<T>()
                .MaxAsync(c => (int?)c.Order, cancellationToken) ?? 0) + 1;
        
        return order;
    }
}