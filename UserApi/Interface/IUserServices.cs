using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace UserApi;

/// <summary>
/// Provides user-related business logic such as registration and login.
/// Handles user operations.
/// /// Defines methods for user registration and authentication.
/// </summary>
    public interface IUserService 
    {
       Task<ServiceResult<string>> CreateUser(SignUpDTO dto);
       Task<ServiceResult<string>> Login(LogInDTO dto);
        Task<ServiceResult<IEnumerable<UserDTO>>> GetAllUsers();
        
    }

 


