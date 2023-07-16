using RestauranteStore.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteStore.Infrastructure.Services.CategoryService
{
	public interface ICategoryService
	{
		Category? GetCategory(int id);
		Category? GetCategory(string name);
		List<Category>? GetCategories();
		int CreateCategory(Category category);
		int UpdateCategory(Category? category);
		Category? DeleteCategory(int id);
		Category? DeleteCategory(string name);


	}
}
