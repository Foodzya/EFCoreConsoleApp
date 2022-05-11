using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceStore.Domain.Entities;

namespace EcommerceStore.Domain.Interfaces
{
    public interface ISectionRepository
    {
        public Task<Section> GetByIdAsync(int sectionId);
        public Task<List<Section>> GetAllAsync();
        public Task<Section> GetByNameAsync(string sectionName);
        public Task CreateAsync(Section section);
        public void Remove(Section section);
        public void Update(Section section);
        public Task SaveChangesAsync();
    }
}