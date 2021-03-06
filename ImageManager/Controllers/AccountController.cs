﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ImageManager.EntityFramework.Models;
using ImageManager.Services.Repositories.AccountRepository;
using ImageManager.Services.DTOModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace ImageManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("/token")]
        public IActionResult Token(string username, string password)
        {
            UserDTO userDTO = _accountRepository.LoginProfile(username, password);
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                role = userDTO.Role.ToString(),
                email = userDTO.Email
            };

            return Json(response);
        }
        private ClaimsIdentity GetIdentity(string username, string password)
        {
            UserDTO User = _accountRepository.LoginProfile(username, password);
            if (User != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, User.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, User.Role.ToString()),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        [HttpPost("/register")]
        public IActionResult RegisterUser(string login, string email, string password)
        {
            _accountRepository.RegisterUser(email, login, password);
            return StatusCode(200);
        }

        [Authorize]
        [HttpGet("/authentication")]
        public IActionResult CheckAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _accountRepository.GetProfile(User.Identity.Name);
                var response = new
                {
                    authStatus = User.Identity.IsAuthenticated,
                    username = user.Login,
                    email = user.Email,
                    role = user.Role.ToString()
                };
                return Json(response);
            }
            return StatusCode(401);
        }
    }
}