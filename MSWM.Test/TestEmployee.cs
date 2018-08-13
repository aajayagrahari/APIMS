using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSWM.BusinessProcessor.Processor;
using MSWM.Data;
using NSubstitute;

namespace MSWM.Test
{
    [TestClass]
    public class TestEmployee
    {
        private Employee _Employee;
        private IDataAccessLayer _IDataAccessLayer;

        [TestInitialize]
        public void Init()
        {
            _IDataAccessLayer = Substitute.For<IDataAccessLayer>();
            _Employee = Substitute.For<Employee>
              (_IDataAccessLayer);
        }

        [TestCategory("Employee"), TestMethod]
        [DataRow(1)]
        public void GetEmployeeByID(int id)
        {
           // _projectRepository.GetAll(x => x.Active).ReturnsForAnyArgs(ProjectList());
           

            var result = _Employee.GetEmployee(id);
            Assert.AreEqual(id, result);

        }
    }
}
