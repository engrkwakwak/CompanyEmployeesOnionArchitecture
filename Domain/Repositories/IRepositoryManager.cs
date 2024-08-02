namespace Domain.Repositories;

public interface IRepositoryManager
{
    ICompanyRepository Company { get; }
    IEmployeeRepository Repository { get; }
    void Save();
}
