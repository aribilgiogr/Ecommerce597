using Core.Abstracts.IServices;
using Core.Concretes.DTOs.Common;
using Core.Concretes.Entities;
using Core.Utils.GenericRepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SelectionItemDto>> GetBrandsForSelectionAsync()
        {
            var brandRepository = unitOfWork.Repository<Brand>();
            var brands = await brandRepository.GetManyAsync(b => b.Active && !b.Deleted);
            return brands.Select(b => new SelectionItemDto { Id = b.Id, Name = b.Name });
        }
    }
}
