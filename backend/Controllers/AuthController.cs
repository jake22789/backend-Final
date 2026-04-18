using final_proj.DTO;
using final_proj.Services;
using final_proj.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace final_proj.Controllers;

[ApiController]
[AllowAnonymous]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _authService.RegisterAsync(dto);
            return Created("/auth/register", new { Username = dto.Username });
        }
        catch (ApiException ex)
        {
            return MapApiException(ex);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var token = await _authService.LoginAsync(dto);
            return Ok(token);
        }
        catch (ApiException ex)
        {
            return MapApiException(ex);
        }
    }

    private IActionResult MapApiException(ApiException ex)
    {
        return ex switch
        {
            BadRequestApiException => BadRequest(new ApiErrorResponseDto { Error = ex.Message }),
            UnauthorizedApiException => Unauthorized(new ApiErrorResponseDto { Error = ex.Message }),
            NotFoundApiException => NotFound(new ApiErrorResponseDto { Error = ex.Message }),
            ConflictApiException => Conflict(new ApiErrorResponseDto { Error = ex.Message }),
            _ => BadRequest(new ApiErrorResponseDto { Error = ex.Message })
        };
    }
}
