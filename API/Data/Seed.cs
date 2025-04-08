using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(DataContext context)
    {
        //return if there is already users recorded in the database.
        if(await context.Users.AnyAsync()) return;

        //Reading from the json file
        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

        //making it case insensitive
        var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        
        //getting the list of users from the json file
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData,options);
        
        //returning if there is no user in the json file
        if(users == null) return;
        
        //making each user name to lower and creating the password.
        foreach(var user in users)
        {
            using var hmac = new HMACSHA512();
            user.UserName = user.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            user.PasswordSalt = hmac.Key;

            context.Users.Add(user);
        }

        await context.SaveChangesAsync();




    }

}
