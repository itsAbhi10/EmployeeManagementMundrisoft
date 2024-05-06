namespace EmployeeManagement.Models
{
    public class ForgotPassword
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
