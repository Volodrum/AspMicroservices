using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ������ ������� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()    // �������� ����-��� ���������� (Origin)
                   .AllowAnyMethod()    // �������� ����-�� ������ (GET, POST, PUT, DELETE ����)
                   .AllowAnyHeader();   // �������� ����-�� ���������
        });
});

// ������������ JWT
var key = "Key1111111111111111111111111111111111"; // ���� ��� ������ ������
var issuer = "MyLocalGenerate"; // ��������
var audience = "MyGroupUser";   // ��������

// ������������ Minimal API
var app = builder.Build();


app.UseCors("AllowAllOrigins");

// �������� ��� ����� �� ��������� JWT ������
app.MapPost("/login", (UserLogin userLogin) =>
{
    // ������ �������� ����� (� ��������� ������� ����� ���� ���������)
    if (userLogin.Username == "testuser" && userLogin.Password == "testpassword")
    {
        var token = GenerateJwtToken(userLogin.Username);
        return Results.Ok(new { token });
    }

    return Results.Unauthorized();
});

// ����� ��������� JWT ������
string GenerateJwtToken(string username)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var keyBytes = Encoding.UTF8.GetBytes(key);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),
        Expires = DateTime.UtcNow.AddMinutes(30), // ����� 䳿 ������
        Issuer = issuer,
        Audience = audience,
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}

app.Run();

// ������ ��� �����
public record UserLogin(string Username, string Password);
