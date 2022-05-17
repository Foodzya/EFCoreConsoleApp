using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceStore.Application.Exceptions;
using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using EcommerceStore.Domain.Entities;
using EcommerceStore.Domain.Interfaces;

namespace EcommerceStore.Application.Services
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public SectionService(ISectionRepository sectionRepository, IProductCategoryRepository productCategoryRepository)
        {
            _sectionRepository = sectionRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task CreateSectionAsync(SectionInputModel sectionInputModel)
        {
            var existingSection = await _sectionRepository.GetByNameAsync(sectionInputModel.Name);

            if (existingSection != null)
                throw new ValidationException(AlreadyExistExceptionMessages.SectionAlreadyExists);

            var section = new Section
            {
                Name = sectionInputModel.Name
            };

            await _sectionRepository.CreateAsync(section);

            await _sectionRepository.SaveChangesAsync();
        }

        public async Task<List<SectionViewModel>> GetAllSectionsAsync()
        {
            var sections = await _sectionRepository.GetAllAsync();

            if (sections is null)
                throw new ValidationException(NotFoundExceptionMessages.SectionNotFound);

            var sectionsViewModel = sections
                .Select(s => new SectionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    ProductCategories = s.ProductCategorySections
                        .Select(p => new ProductCategoryViewModel
                        {
                            Id = p.ProductCategoryId,
                            Name = p.ProductCategory.Name
                        })
                        .ToList()
                })
                .ToList();

            return sectionsViewModel;
        }

        public async Task<SectionViewModel> GetSectionByIdAsync(int sectionId)
        {
            var section = await _sectionRepository.GetByIdAsync(sectionId);

            if (section is null)
                throw new ValidationException(NotFoundExceptionMessages.SectionNotFound, sectionId);

            var sectionViewModel = new SectionViewModel
            {
                Name = section.Name,
                ProductCategories = section.ProductCategorySections
                    .Select(p => new ProductCategoryViewModel
                    {
                        Id = p.ProductCategoryId,
                        Name = p.ProductCategory.Name
                    })
                    .ToList()
            };

            return sectionViewModel;
        }

        public async Task LinkCategoryToSectionAsync(ProductCategoryInputModel productcategoryIm)
        {
            var section = await _sectionRepository.GetByIdAsync(productcategoryIm.SectionId);

            if (section is null)
                throw new ValidationException(NotFoundExceptionMessages.SectionNotFound, productcategoryIm.SectionId);

            var parentProductCategory = await _productCategoryRepository.GetByIdAsync((int)productcategoryIm.ParentCategoryId);

            if (parentProductCategory != null)
            {
                var parentProductCategorySectionIds = section.ProductCategorySections
                    .Select(s => s.SectionId)
                    .ToList();

                if (!parentProductCategorySectionIds.Contains(productcategoryIm.SectionId))
                    throw new ValidationException(ExceptionMessages.LinkCategoryToSectionFailed);
            }

            section.ProductCategorySections.Add(new ProductCategorySection
            {
                ProductCategoryId = (int)productcategoryIm.ParentCategoryId,
                SectionId = productcategoryIm.SectionId
            });

            _sectionRepository.Update(section);

           await _sectionRepository.SaveChangesAsync();
        }

        public async Task RemoveSectionAsync(int sectionId)
        {
            var section = await _sectionRepository.GetByIdAsync(sectionId);

            if (section is null)
                throw new ValidationException(NotFoundExceptionMessages.SectionNotFound, sectionId);

            section.IsDeleted = true;

            _sectionRepository.Update(section);

            await _sectionRepository.SaveChangesAsync();
        }

        public async Task RenameSectionAsync(int sectionId, SectionInputModel sectionInputModel)
        {
            var existingSection = await _sectionRepository.GetByNameAsync(sectionInputModel.Name);

            if (existingSection != null && existingSection.Id != sectionId)
                throw new ValidationException(AlreadyExistExceptionMessages.SectionAlreadyExists);

            var section = await _sectionRepository.GetByIdAsync(sectionId);

            section.Name = sectionInputModel.Name;

            _sectionRepository.Update(section);

            await _sectionRepository.SaveChangesAsync();
        }
    }
}