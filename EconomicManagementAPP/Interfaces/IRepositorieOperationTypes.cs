using EconomicManagementAPP.Models;

public interface IRepositorieOperationTypes
{
    Task Create(OperationTypes operationTypes);

    Task<bool> Exist(string Description);

    Task<IEnumerable<OperationTypes>> getOperation();

    Task Modify(OperationTypes operationTypes);

    Task<OperationTypes> getOperationById(int id); // para el modify

    Task Delete(int id);

}
