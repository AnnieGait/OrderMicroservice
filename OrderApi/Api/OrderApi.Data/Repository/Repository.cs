using OrderApi.Data.Database;

namespace OrderApi.Data.Repository
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
	{
		private readonly OrderContext _orderContext;

		public Repository(OrderContext orderContext)
		{
			_orderContext = orderContext;
		}

		public IQueryable<TEntity> GetAll()
		{
			return _orderContext.Set<TEntity>();
		}

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			ArgumentNullException.ThrowIfNull(entity, $"{nameof(AddAsync)} entity must not be null");

			await _orderContext.AddAsync(entity);
			await _orderContext.SaveChangesAsync();

			return entity;
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			ArgumentNullException.ThrowIfNull(entity, $"{nameof(UpdateAsync)} entity must not be null");

			_orderContext.Update(entity);
			await _orderContext.SaveChangesAsync();

			return entity;
		}

		public async Task UpdateRangeAsync(List<TEntity> entities)
		{
			ArgumentNullException.ThrowIfNull(entities, $"{nameof(UpdateRangeAsync)} entities must not be null");

			_orderContext.UpdateRange(entities);
			await _orderContext.SaveChangesAsync();
		}
	}
}