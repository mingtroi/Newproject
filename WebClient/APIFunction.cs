using System.Text;
using WebClient.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebClient
{
    public class APIFunction
    {
        public static List<EmployeeDTO> GetAllEmployee()
        {
            List<EmployeeDTO> employee = new List<EmployeeDTO>();
            HttpClient client = new HttpClient();
            string url = "https://localhost:7122/api/Employee/GetAllEmployee";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //    string responseData = response.Content.ReadAsStringAsync().Result;
                //    employee = System.Text.Json.JsonSerializer.Deserialize<EmployeeDTO>(responseData);
                employee = response.Content.ReadFromJsonAsync<List<EmployeeDTO>>().Result;
            }
            return employee;
        }

        public static EmployeeDTO GetEmployeeById(int id)
        {
            EmployeeDTO employee = new EmployeeDTO();
            HttpClient client = new HttpClient();
            string url = $"https://localhost:7122/api/Employee/GetEmployeeById/{id}";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                employee = response.Content.ReadFromJsonAsync<EmployeeDTO>().Result;
            }
            return employee;
        }

        public static int CreateEmployee(EmployeeDTO employee)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:7122/api/Employee/CreateEmployee";
            string data = System.Text.Json.JsonSerializer.Serialize(employee);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, httpContent).GetAwaiter().GetResult();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return 200;
            }
            return -1;
        }
        public static int UpdateEmployee(EmployeeDTO employee)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:7122/api/Employee/UpdateEmployee";
            string data = System.Text.Json.JsonSerializer.Serialize(employee);
            HttpContent httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, httpContent).GetAwaiter().GetResult();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return 200;
            }
            return -1;
        }

        public static int DeleteEmployee(int id)
        {
            HttpClient client = new HttpClient();
            string url = $"https://localhost:7122/api/Employee/DeleteEmployee/{id}";
            HttpResponseMessage response = client.DeleteAsync(url).GetAwaiter().GetResult();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return 200;
            }
            return -1;
        }

        public static List<DepartmentDTO> GetAllDepartments()
        {
            List<DepartmentDTO> departments = new List<DepartmentDTO>();
            HttpClient client = new HttpClient();
            string url = "https://localhost:7122/api/Departments/GetAllDepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                departments = response.Content.ReadFromJsonAsync<List<DepartmentDTO>>().Result;
            }
            return departments;
        }
        public static DepartmentDTO GetDepartmentById(int id)
        {
            DepartmentDTO department = new DepartmentDTO();
            HttpClient client = new HttpClient();
            string url = $"https://localhost:7122/api/Departments/GetDepartmentById/{id}";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                department = response.Content.ReadFromJsonAsync<DepartmentDTO>().Result;
            }
            return department;
        }

        public static int CreateDepartment(DepartmentDTO department)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:7122/api/Departments/CreateDepartment";
            string data = System.Text.Json.JsonSerializer.Serialize(department);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, httpContent).GetAwaiter().GetResult();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return 200;
            }
            return -1;
        }
        public static int UpdateDepartment(DepartmentDTO department)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:7122/api/Departments/UpdateDepartment";
            string data = System.Text.Json.JsonSerializer.Serialize(department);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, httpContent).GetAwaiter().GetResult();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return 200;
            }
            return -1;
        }

        public static int DeleteDepartment(int id)
        {
            HttpClient client = new HttpClient();
            string url = $"https://localhost:7122/api/Departments/DeleteDepartment/{id}";
            HttpResponseMessage response = client.DeleteAsync(url).GetAwaiter().GetResult();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return 200;
            }
            return -1;
        }
        public static int AddEmployeeToDepartment(int departmentId, int employeeId)
        {
                using (var client = new HttpClient())
                {
                    // Đổi thứ tự tham số cho đúng với controller
                    string url = $"https://localhost:7122/api/Departments/AddEmployeeToDepartment/{departmentId}/{employeeId}";

                    // Sử dụng await với PostAsync
                    HttpResponseMessage response = client.PostAsync(url, null).Result;

                    response.EnsureSuccessStatusCode();

                    // Sử dụng await với ReadAsStringAsync
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return 200;
                    }
                    return -1;
                }
        }
        public static int RemoveEmployeeFromDepartment(int departmentId, int employeeId)
        {
            HttpClient client = new HttpClient();
            string url = $"https://localhost:7122/api/Departments/RemoveEmployeeFromDepartment/{departmentId}/{employeeId}";
            HttpResponseMessage response = client.PostAsync(url, null).GetAwaiter().GetResult();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return 200;
            }
            return -1;
        }

        public static List<SalaryDTO> GetAllSalaries()
        {
            List<SalaryDTO> salaries = new List<SalaryDTO>();
            HttpClient client = new HttpClient();
            string url = "https://localhost:7122/api/Salary/GetAllSalaries";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                salaries = response.Content.ReadFromJsonAsync<List<SalaryDTO>>().Result;
            }
            return salaries;
        }

        public static int CreateSalaries(SalaryDTO salary)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:7122/api/Salary/CreateSalaries";
            string data = System.Text.Json.JsonSerializer.Serialize(salary);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, httpContent).GetAwaiter().GetResult();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return 200;
            }
            return -1;
        }

    }
}
