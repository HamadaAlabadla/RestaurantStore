using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using NToastNotify;

namespace RestauranteStore.Infrastructure.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IToastNotification toastNotification;

        public CategoryService(ApplicationDbContext dbContext, IToastNotification toastNotification)
        {
            this.dbContext = dbContext;
            this.toastNotification = toastNotification;
        }

        public int CreateCategory(Category category)
        {
            var categoryOld = GetCategory(category.Name ?? "");
            if (categoryOld != null) 
            {
                toastNotification.AddErrorToastMessage("Category already exists.");
                return -1;
            }
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            toastNotification.AddSuccessToastMessage("Category created successfully.");
            return category.Id;
        }

        public Category? DeleteCategory(int id)
        {
            var category = GetCategory(id);
            if (category == null) 
            {
                toastNotification.AddErrorToastMessage("Category does not exist.");
                return null;
            }
            category.isDelete = true;
            UpdateCategory(category);
            toastNotification.AddSuccessToastMessage("Category deleted successfully.");
            return category;
        }
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
            if (category == null) 
            {
                toastNotification.AddErrorToastMessage("Category does not exist.");
                return -1;
            }
            var categoryNew = GetCategory(category.Id);
            if (categoryNew == null) 
            {
                toastNotification.AddErrorToastMessage("Category does not exist.");
                return -1;
            }
            categoryNew.isDelete = category.isDelete;
            categoryNew.Name = category.Name;
            dbContext.Categories.Update(categoryNew);
            dbContext.SaveChanges();
            toastNotification.AddSuccessToastMessage("Category updated successfully.");
            return category.Id;
        }
    }
}