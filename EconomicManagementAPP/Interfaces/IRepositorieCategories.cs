using EconomicManagementAPP.Models;

public interface IRepositorieCategories
{
    Task Create(Categories categories);

    Task<bool> Exist(string Name);

    Task<IEnumerable<Categories>> getCategories();

    Task<IEnumerable<Categories>> GetCategories(int userId);

    Task<IEnumerable<Categories>> GetCategories(int userId, OperationTypes operationTypes);

    Task Modify(Categories categories);

    Task<Categories> getCategorieById(int id);

    Task Delete(int id);

}
