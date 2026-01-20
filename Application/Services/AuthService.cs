using API.Entities;
using API.Interfaces;
using Application.Validators;
using AuthMicroservice.Dtos;
using FluentValidation;
using Serilog;

namespace AuthMicroservice.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _authRepo;
    private readonly IValidator<RegisterRequest> _requestValidator;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IJWTTokenGenerator _tokenGen;
    private readonly IPasswordHashAssist _passwordHasher;

    public AuthService(
        IUserRepository authRepo,
        RegisterRequestValidator requestRequestValidator,
        LoginRequestValidator loginValidator,
        IJWTTokenGenerator tokenGen,
        IPasswordHashAssist passwordHasher
    )
    {
        _authRepo = authRepo;
        _requestValidator = requestRequestValidator;
        _loginValidator = loginValidator;
        _tokenGen = tokenGen;
        _passwordHasher = passwordHasher;
    }

    public RegisterResponseDetails RegisterUser(RegisterRequest user)
    {
        try
        {
            Log.Information("User Registration started. Email: {Email}", user.Email);

            var validationResult = _requestValidator.Validate(user);
            if (!validationResult.IsValid)
            {
                return new RegisterResponseDetails
                {
                    Message = "Validation failed",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    IsSuccess = false
                };
            }

            var userRegisterDetails = new User
            {
                Email = user.Email,
                UserName = user.UserName,
                PhoneNo = user.PhoneNo,
                DOB = user.DOB,
                GenderId = user.GenderId,
                ProfileImageUrl = user.ProfileImageUrl,
                HashedPassword = _passwordHasher.HasherPassword(user.Password)
            };

            var createdRecord = _authRepo.AddUser(userRegisterDetails);

            if (createdRecord == null)
            {
                Log.Error("User registration failed. Email: {Email}", user.Email);

                return new RegisterResponseDetails
                {
                    Message = "An error occurred. Try again later",
                    IsSuccess = false
                };
            }

            Log.Information("User registration successful. Email: {Email}", user.Email);

            return new RegisterResponseDetails
            {
                Message = "Registered successfully",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Unexpected error during registration. Email: {Email}", user.Email);

            return new RegisterResponseDetails
            {
                Message = "An unexpected error occurred",
                IsSuccess = false
            };
        }
    }

    public LoginResponseDetails Login(LoginRequest loginRequest)
    {
        try
        {
            Log.Information("Login attempt for {Email}", loginRequest.Email);

            var validationResult = _loginValidator.Validate(loginRequest);
            if (!validationResult.IsValid)
            {
                return new LoginResponseDetails
                {
                    Message = "Invalid login request",
                    IsSuccess = false
                };
            }

            var hashedPassword = _passwordHasher.HasherPassword(loginRequest.Password);

            var user = _authRepo.GetBy(u =>
                u.Email == loginRequest.Email &&
                u.HashedPassword == hashedPassword);

            if (user == null)
            {
                Log.Warning("Invalid login attempt for {Email}", loginRequest.Email);

                return new LoginResponseDetails
                {
                    Message = "Invalid email or password",
                    IsSuccess = false
                };
            }

            var token = _tokenGen.GenerateToken(user.Email);

            Log.Information("User {Email} logged in successfully", user.Email);

            return new LoginResponseDetails
            {
                Message = "Login successful",
                IsSuccess = true,
                Token = token
            };
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Login failed for {Email}", loginRequest.Email);

            return new LoginResponseDetails
            {
                Message = "An unexpected error occurred",
                IsSuccess = false
            };
        }
    }
}
