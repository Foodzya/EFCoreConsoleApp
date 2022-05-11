using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;

namespace EcommerceStore.Application.Interfaces
{
    public interface ISectionService
    {
        public Task<List<SectionViewModel>> GetAllSectionsAsync();
        public Task<SectionViewModel> GetSectionByIdAsync(int sectionId);
        public Task UpdateSectionAsync(int sectionId, SectionInputModel sectionInputModel);
        public Task RemoveSectionAsync(int sectionId);
        public Task CreateSectionAsync(SectionInputModel sectionInputModel);
    }
}