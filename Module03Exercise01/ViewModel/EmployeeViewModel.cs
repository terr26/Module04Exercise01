using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Module03Exercise01.Model;

namespace Module03Exercise01.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private Employee _employee;
        private string _fullName;
        private string _managerName;
        private string _managerDepartment;

        public EmployeeViewModel()
        {
            // Initialize the employee without setting ManagerName and Department initially
            _employee = new Employee
            {
                FirstName = "Eddie",
                LastName = "Ehrlich",
                Position = "Manager",
                Department = "Sales",
                IsActive = true
            };

            LoadEmployeeDataCommand = new Command(async () => await LoadEmployeeDataAsync());

            Employees = new ObservableCollection<Employee>
            {
                new Employee {FirstName="Gertrude", LastName="Riley", Position="Back End Developer", Department="IT", IsActive=true},
                new Employee {FirstName="Emma", LastName="Key", Position="Front End Developer", Department="IT", IsActive=false},
                new Employee {FirstName="Antoinette", LastName="Curtis", Position="UI/UX Designer", Department="Marketing", IsActive=true}
            };
        }

        public ObservableCollection<Employee> Employees { get; set; }

        public string FullName
        {
            get => _fullName;
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public string ManagerName
        {
            get => _managerName;
            set
            {
                if (_managerName != value)
                {
                    _managerName = value;
                    OnPropertyChanged(nameof(ManagerName));
                }
            }
        }

        public string ManagerDepartment
        {
            get => _managerDepartment;
            set
            {
                if (_managerDepartment != value)
                {
                    _managerDepartment = value;
                    OnPropertyChanged(nameof(ManagerDepartment));
                }
            }
        }

        public ICommand LoadEmployeeDataCommand { get; }

        private async Task LoadEmployeeDataAsync()
        {
            await Task.Delay(1000); // Simulate an I/O Operation
            FullName = $"{_employee.FirstName} {_employee.LastName}";
            ManagerName = _employee.FullName; // Set the manager name when loading data
            ManagerDepartment = _employee.Department; // Set the department when loading data
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
