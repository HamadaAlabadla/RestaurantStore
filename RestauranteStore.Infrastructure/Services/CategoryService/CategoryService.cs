using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteStore.Infrastructure.Services.CategoryService
{
	public class CategoryService : ICategoryService
	{
		private readonly ApplicationDbContext dbContext;
		public CategoryService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public int CreateCategory(Category category)
		{
			var categoryOld = GetCategory(category.Name??"");
			if (categoryOld != null) return -1;
			dbContext.Categories.Add(category);
			dbContext.SaveChanges();
			return category.Id;
		}

		public Category? DeleteCategory(int id)
		{
			var category = GetCategory(id);
			if(category == null) return null;
			category.isDelete = true;
			UpdateCategory(category);
			return category;

		}

		public Category? DeleteCategory(string name)
		{
			var category = GetCategory(name);
			if (category == null) return null;

			//if(dbContext.dfgsd.Any(x => x.Catego != category.Id))
			//{
			//	return null;
			//}
			category.isDelete = true;
			UpdateCategory(category);
			return category;
		}

		public List<Category>? GetCategories()
		{
			return dbContext.Categories.Where(x => !x.isDelete).ToList();
		}

		public Category? GetCategory(int id)
		{
			return dbContext.Categories.Where(x => !x.isDelete).FirstOrDefault(x => x.Id == id);
		}

		public Category? GetCategory(string name)
		{
			return dbContext.Categories.Where(x => !x.isDelete).FirstOrDefault(x => (x.Name ?? "").Equals(name));
		}

		public int UpdateCategory(Category? category)
		{
			if (category == null) return -1;
			var categoryNew = GetCategory(category.Id);
			if(categoryNew == null) return -1;
			categoryNew.isDelete = category.isDelete;
			categoryNew.Name = category.Name;
			dbContext.Categories.Update(categoryNew);
			dbContext.SaveChanges();
			return category.Id;
		}
	}
}
