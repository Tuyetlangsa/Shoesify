

namespace Shoesify.Services.Requests
{
    public class UpdateUserRequest
    {
        public string userId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}
