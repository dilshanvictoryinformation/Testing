using ERP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestApp.ServiceReference1;


namespace TestApp
{
    class MainViewModel : ViewModelBase
    {
        #region Fields
        EmpserviceClient EmpService;

        #endregion

        #region Constructor

        public MainViewModel()
        {
            EmpService = new EmpserviceClient();
            RefreshEmployees();
            CurrentEmployee = new Employee();
        }

        #endregion

        #region Properties

        private IEnumerable<Employee> _Employee;

        public IEnumerable<Employee> Employee
        {
            get { return _Employee; }
            set { _Employee = value; OnPropertyChanged("Employee"); }
        }

        private Employee _CurrentEmployee;

        public Employee CurrentEmployee
        {
            get { return _CurrentEmployee; }
            set { _CurrentEmployee = value; OnPropertyChanged("CurrentEmployee"); }
        }

        #endregion

        #region RefreshMethods

        private void RefreshEmployees()
        {
            EmpService.GetEmployeeCompleted += (s, e) =>
                {
                    Employee = e.Result;
                };
            EmpService.GetEmployeeAsync();
        }


        #endregion

        #region Button Commands
        public ICommand SaveButton
        {
            get { return new RelayCommand(Save); }
        }

        #endregion

        #region Methods

        private void Save()
        {
            if (CurrentEmployee.Emp_id ==0)
            {
                if (EmpService.save_employee(CurrentEmployee))
                {
                    MessageBox.Show("Success");
                    RefreshEmployees();
                    CurrentEmployee = new Employee();
                }
                else
                    MessageBox.Show("Fail"); 
            }
            else
            {
                if(EmpService.Update_Employee(CurrentEmployee))
                {
                    MessageBox.Show("Update Success");
                    RefreshEmployees();
                    CurrentEmployee = new Employee();
                }
                else
                    MessageBox.Show("Update Fail"); 

            }
            
        }

        #endregion
    }
}
