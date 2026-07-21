using Core.Concretes.DTOs.Common;

namespace Core.Abstracts.IServices
{
    public interface IBrandService
    {
        Task<IEnumerable<SelectionItemDto>> GetBrandsForSelectionAsync();
    }
}
