using CustomerApi.Data.Database;

namespace CustomerApi.Data.Repositories
{
	public class CustomerRepository<TEntity> : ICustomerRepository<TEntity> where TEntity : class, new()
	{
		private readonly CustomerContext _customerContext;

		public CustomerRepository(CustomerContext customerContext)
		{
			_customerContext = customerContext;
		}

		public IQueryable<TEntity> GetAll()
		{
			try
			{
				return _customerContext.Set<TEntity>();
			}
			catch (Exception)
			{
				throw new Exception("Couldn't retrieve entities");
			}
		}

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			ArgumentNullException.ThrowIfNull(entity);

			try
			{
				await _customerContext.AddAsync(entity);
				await _customerContext.SaveChangesAsync();

				return entity;
			}
			catch (Exception)
			{
				throw new Exception($"{nameof(entity)} could not be saved");
			}
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			ArgumentNullException.ThrowIfNull(entity);

			try
			{
				_customerContext.Update(entity);
				await _customerContext.SaveChangesAsync();

				return entity;
			}
			catch (Exception)
			{
				throw new Exception($"{nameof(entity)} could not be updated");
			}
		}
	}
}
