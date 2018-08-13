using MSWM.BusinessProcessor.Dtos;
using System.Collections.Generic;

namespace MSWM.BusinessProcessor.Interface
{
    /// <summary>
    /// contains a signature for all required operations on Project
    /// </summary>
    public interface IEmployee
    {
        ICollection<EmployeeDto> GetEmployee(int id);

        //AddEmployeeResDto AddEmployee(EmployeeDto addEmployeeDto);

        //AddEmployeeResDto UpdateEmployee(EmployeeDto addEmployeeDto);

        //int DeleteEmployee(int id);      
    }
}
