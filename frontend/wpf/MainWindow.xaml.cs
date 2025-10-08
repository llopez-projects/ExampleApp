using core.DTOs;
using core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IEmployeeService _employeeService;
    private EmployeeDto _employee;
    public MainWindow()
    {
        InitializeComponent();
        _employeeService = App.ServiceProvider.GetRequiredService<IEmployeeService>();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        var employees = await _employeeService.GetAllAsync();
        lstEmployees.ItemsSource = employees;
    }

    private void lstEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _employee = lstEmployees.SelectedItem as EmployeeDto;
        if (_employee != null)
        {
            txtFullName.Text = _employee.FullName;
            txtEmail.Text = _employee.Email;
            txtDateHired.Text = _employee.DateHired.ToString();
            txtDepartment.Text = _employee.DepartmentName;
        }
    }
}

