using Core.Abstracts.IServices;
using Core.Concretes.DTOs.Common;
using Core.Concretes.Entities;
using Core.Utils.GenericRepositoryPattern;

namespace Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SelectionItemDto>> GetCategoriesForSelectionAsync()
        {
            var categoryRepository = unitOfWork.Repository<Category>();
            var allCategories = await categoryRepository.GetManyAsync(c => c.Active && !c.Deleted);

            return allCategories.Select(c => new SelectionItemDto
            {
                Id = c.Id,
                Name = GetCategoryPath(c, allCategories)
            }).OrderBy(c => c.Name);
        }

        private string GetCategoryPath(Category category, IEnumerable<Category> allCategories)
        {
            var parent = allCategories.FirstOrDefault(c => c.Id == category.ParentCategoryId);
            return parent == null ? category.Name : $"{GetCategoryPath(parent, allCategories)} > {category.Name}";
        }
    }
}
