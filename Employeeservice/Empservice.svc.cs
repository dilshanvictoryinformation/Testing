using Empdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace Employeeservice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Empservice" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Empservice.svc or Empservice.svc.cs at the Solution Explorer and start debugging.
    [ServiceContract]
    public class Empservice
    {
        [OperationContract]
        public IEnumerable<Employee> GetEmployee()
        {
            using (var context = new Test1Entities())
            {
                List<Employee> result = context.Employees.ToList();
                result.ForEach(d => context.Detach(d));
                return result;
            }
        }

        [OperationContract]
        public IEnumerable<City> GetCity()
        {
            using (var context = new Test1Entities())
            {
                var result = context.Cities.ToList();
                result.ForEach(c => context.Detach(c));
                return result;
            }
        }

        [OperationContract]
        public bool save_employee(Employee SaveObj)
        {
            using (var context = new Test1Entities())
            {
                try
                {
                    context.Employees.AddObject(SaveObj);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        [OperationContract]
        public bool Save_city(City CityObj)
        {
            using (var context = new Test1Entities())
                try
                {
                    context.Cities.AddObject(CityObj);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
        }


        [OperationContract]
        public bool Update_Employee(Employee UpdateObj)
        {
            try
            {
                using (var context = new Test1Entities())
                {
                    Employee TempObj = context.Employees.FirstOrDefault(c => c.Emp_id == UpdateObj.Emp_id);
                    TempObj.NAme = UpdateObj.NAme;
                    TempObj.Email = UpdateObj.Email;
                    TempObj.Address = UpdateObj.Address;
                    context.SaveChanges();
                    return true;

                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        [OperationContract]
        public IEnumerable<Employee>GeTEmp()
        {
            using (var context = new Test1Entities())
            {
                try
                {
                    var result = context.Employees.Where(c => c.Emp_id == 2).ToList();
                    result.ForEach(d => context.Detach(d));
                    return result;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
    }
}
