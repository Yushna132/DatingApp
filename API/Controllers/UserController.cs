using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")] // /api/users
public class UserController(DataContext context) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        return context.Users.ToList();
    }

    [HttpGet("{id}")]   //api/users/3
    public ActionResult<AppUser> GetUser(int id)
    {
        var user = context.Users.Find(id);
        return user != null ? user : NotFound();
    }
}
