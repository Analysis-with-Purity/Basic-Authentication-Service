using API.Entities;
using API.Interfaces;
using Application.Validators;
using AuthMicroservice.Dtos;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AuthMicroservice.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _authRepo;
    private readonly IMapper _mapper;
    private readonly IValidator<RegisterRequest> _validator;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IJWTTokenGenerator _tokenGen;
    private readonly IPasswordHashAssist _passwordHasher;

    public AuthService(
        IUserRepository authRepo,
        IMapper mapper,
        RegisterRequestValidator requestValidator,
        LoginRequestValidator loginValidator,
        IUserRepository userAuth,
        IJWTTokenGenerator tokenGen
    )
    {
        _authRepo = authRepo;
        _mapper = mapper;
        _validator = requestValidator;
        _loginValidator = loginValidator;
        _tokenGen = tokenGen;
    }

    public RegisterResponseDetails RegisterUser(RegisterRequest user)
    {
        try
        {
            Log.Information("User Registration started. Email address: {Email}", user.Email);

            var response = new RegisterResponseDetails();
            var validationResult = _validator.Validate(user);

            if (!validationResult.IsValid)
            {
                Log.Warning("Validation failed for this reason: {@Errors}", validationResult.Errors);

                response.Message = "Validation failed";
                response.Errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return response;
            }

            var userRegisterDetails = _mapper.Map<User>(user);

            userRegisterDetails.HashedPassword = _passwordHasher.HasherPassword(user.Password);

            var createdRecord = _authRepo.AddUser(userRegisterDetails);

            if ((createdRecord = null) != null)
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
                Message = "Entered successfully.",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Unexpected error during user registration for Email: {Email}", user.Email);

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
            Log.Information("Login attempt for {email}", loginRequest.EmailAddress);
            var validationResult = _loginValidator.Validate(loginRequest);
            if (!validationResult.IsValid)
            {
                Log.Warning("Validation failed for email: {Email} for this reason: {@Errors}",
                    loginRequest.EmailAddress, validationResult.Errors);

                return new LoginResponseDetails
                {
                    Message = "Please check the request and try again",
                    IsSuccess = false
                };
            }
            var hashedPassword = IPasswordHashAssist.HasherPassword(loginRequest.Password);

            var user = _authRepo.GetUserByEmail(a => 
                a.EmailAddress == loginRequest.EmailAddress && 
                a.HashedPassword == hashedPassword);
            if (user == null)
            {
                Log.Warning("Invalid login attempt for email: {Email}", loginRequest.EmailAddress);

                return new LoginResponseDetails
                {
                    Message = "Invalid email or password",
                    IsSuccess = false
                };
            }
            var token = _tokenGen.GenerateToken(user.EmailAddress);

            Log.Information("User {Email} login successful", user.EmailAddress);

            return new LoginResponseDetails
            {
                Message = "Successful",
                IsSuccess = true,
                Token = token
            };
            throw new NotImplementedException();
        }
        catch(Exception ex)
        {
            
            
            
            
            throw new NotImplementedException();
        }

    }
}