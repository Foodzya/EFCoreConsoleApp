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

        public SectionService(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task CreateSectionAsync(SectionInputModel sectionInputModel)
        {
            var existingSection = await _sectionRepository.GetByNameAsync(sectionInputModel.Name);

            if (existingSection != null)
                throw new ValidationException(ExceptionMessages.SectionAlreadyExists);

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
                throw new ValidationException(ExceptionMessages.SectionNotFound);

            var sectionsViewModel = sections
                .Select(s => new SectionViewModel
                {
                    Name = s.Name,
                    ProductCategories = s.ProductCategorySections
                        .Where(p => p.ProductCategory.Id == p.ProductCategoryId)
                        .Select(p => new ProductCategoryViewModel
                        {
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
                throw new ValidationException(ExceptionMessages.SectionNotFound, sectionId);

            var sectionViewModel = new SectionViewModel
            {
                Name = section.Name,
                ProductCategories = section.ProductCategorySections
                    .Where(p => p.ProductCategory.Id == p.ProductCategoryId)
                    .Select(p => new ProductCategoryViewModel
                    {
                        Name = p.ProductCategory.Name
                    })
                    .ToList()
            };

            return sectionViewModel;
        }

        public async Task RemoveSectionAsync(int sectionId)
        {
            var section = await _sectionRepository.GetByIdAsync(sectionId);

            if (section is null)
                throw new ValidationException(ExceptionMessages.SectionNotFound, sectionId);

            _sectionRepository.Remove(section);

            await _sectionRepository.SaveChangesAsync();
        }

        public async Task UpdateSectionAsync(int sectionId, SectionInputModel sectionInputModel)
        {
            var existingSection = await _sectionRepository.GetByNameAsync(sectionInputModel.Name);

            if (existingSection != null && existingSection.Id != sectionId)
                throw new ValidationException(ExceptionMessages.SectionAlreadyExists);

            var section = await _sectionRepository.GetByIdAsync(sectionId);

            section.Name = sectionInputModel.Name;

            _sectionRepository.Update(section);

            await _sectionRepository.SaveChangesAsync();
        }
    }
}