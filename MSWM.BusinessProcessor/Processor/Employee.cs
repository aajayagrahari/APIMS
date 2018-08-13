using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWM.BusinessProcessor.Dtos;
using MSWM.BusinessProcessor.Interface;
using MSWM.Common.Constant;
using MSWM.Common.CustomException;
using MSWM.Data;

namespace MSWM.BusinessProcessor.Processor
{
    public class Employee : IEmployee
    {
        private IDataAccessLayer _da;
        DataSet _ds=new DataSet();
        
        
        public Employee(IDataAccessLayer da)
        {
            _da = da;
        }
        public ICollection<EmployeeDto> GetEmployee(int id)
        {
            List<EmployeeDto> lst = new List<EmployeeDto>();
            _ds = GetEmployeeData(id);

            if (_ds != null)
            {
                foreach (DataRow dr in _ds.Tables[0].Rows)
                {
                    lst.Add(objCreateEmployee(dr));
                }
            }
            goto Finish;

            Finish:
            _ds = null;

            return lst;

            //throw new MSWMException("EmployeeNotFound", Message.MSWMDashboard);
        }

        private DataSet GetEmployeeData(int argEmployeeID)
        {
            SqlParameter[] param = new SqlParameter[1]; 
            param[0] = new SqlParameter("@id", argEmployeeID);

            _ds = _da.FillDataSet("GetEmployee", param);
            return _ds;
        }

        private EmployeeDto objCreateEmployee(DataRow dr)
        {
            EmployeeDto _empDto = new EmployeeDto();
            _empDto.id = Convert.ToInt32(dr["id"]);
            _empDto.name = Convert.ToString(dr["name"]);
            _empDto.phone = Convert.ToString(dr["phone"]);
            _empDto.address = Convert.ToString(dr["address"]);
            return _empDto;
        }

    }
}
