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
			try
			{
				return _orderContext.Set<TEntity>();
			}
			catch (Exception)
			{
				throw new Exception("Couldn't retrieve entities");
			}
		}

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			ArgumentNullException.ThrowIfNull(entity, $"{nameof(AddAsync)} entity must not be null");

			try
			{
				await _orderContext.AddAsync(entity);
				await _orderContext.SaveChangesAsync();

				return entity;
			}
			catch (Exception)
			{
				throw new Exception($"{nameof(entity)} could not be saved");
			}
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			ArgumentNullException.ThrowIfNull(entity, $"{nameof(UpdateAsync)} entity must not be null");

			try
			{
				_orderContext.Update(entity);
				await _orderContext.SaveChangesAsync();

				return entity;
			}
			catch (Exception)
			{
				throw new Exception($"{nameof(entity)} could not be updated");
			}
		}

		public async Task UpdateRangeAsync(List<TEntity> entities)
		{
			ArgumentNullException.ThrowIfNull(entities, $"{nameof(UpdateRangeAsync)} entities must not be null");

			try
			{
				_orderContext.UpdateRange(entities);
				await _orderContext.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw new Exception($"{nameof(entities)} could not be updated");
			}
		}
	}
}