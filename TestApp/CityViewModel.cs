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
    class CityViewModel : ViewModelBase
    {
        #region Fields
        EmpserviceClient EmpService;

        #endregion

        #region Constructor
        public CityViewModel()
        {
            EmpService = new EmpserviceClient();
            RefreshCity();
            CurrentCity = new City();
           
        }



        #endregion

        #region Properties
        private IEnumerable<City> _City;

        public IEnumerable<City> City
        {
            get { return _City; }
            set { _City = value; OnPropertyChanged("City"); }
        }

        private City _CurrentCity;

        public City CurrentCity
        {
            get { return _CurrentCity; }
            set { _CurrentCity = value; OnPropertyChanged("CurrentCity"); }
        }
        
        

        #endregion

        #region RefreshMethods
        private void RefreshCity()
        {
            EmpService.GetCityCompleted += (s, e) =>
            {
                City = e.Result;
            };
            EmpService.GetCityAsync();
        }

        #endregion

        #region Buttoncommands
        public ICommand SaveButton
        
        {
            get { 
                return new RelayCommand(save);
                }
        }

        #endregion

        #region Methods
        private void save()
        {
            if (EmpService.Save_city(CurrentCity))
            {
                MessageBox.Show("Success");
                RefreshCity();
                CurrentCity = new City();
                CurrentCity.City_id = 1;
            }
            else MessageBox.Show("Failed");
        }
        #endregion
    }

}
