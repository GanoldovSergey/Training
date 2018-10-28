using System.Collections.Generic;
using System.Threading.Tasks;
using Training.DAL.Entities;

namespace Training.DAL.Services.Interfaces
{
    public interface ITestRepository
    {
        Task CreateTestAsync(TestDto test);

        Task DeleteTestAsync(string id);

        Task<TestDto> GetTestByIdAsync(string id);

        Task<IEnumerable<TestDto>> GetTestsAsync();

        Task UpdateTestAsync(string id, TestDto test);

        Task<bool> IsTestExistAsync(TestDto test);
    }
}
