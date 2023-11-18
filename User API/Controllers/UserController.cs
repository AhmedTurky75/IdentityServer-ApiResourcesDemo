using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User_API.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private static List<User> _users = new List<User>
    {
        new User { Id = 1, FirstName = "John", LastName = "Doe" },
        new User { Id = 2, FirstName = "Jane", LastName = "Smith" }
        // Add more initial users as needed
    };

    [Authorize("read")]
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(_users);
    }

    [Authorize("read")]
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [Authorize("write")]
    [HttpPost]
    public IActionResult CreateUser([FromBody] User newUser)
    {
        if (newUser == null)
        {
            return BadRequest("Invalid user data");
        }

        newUser.Id = GenerateUserId();
        _users.Add(newUser);

        return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
    }

    [Authorize("write")]
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == id);

        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.FirstName = updatedUser.FirstName;
        existingUser.LastName = updatedUser.LastName;

        return Ok(existingUser);
    }

    [Authorize("write")]
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var userToRemove = _users.FirstOrDefault(u => u.Id == id);

        if (userToRemove == null)
        {
            return NotFound();
        }

        _users.Remove(userToRemove);

        return NoContent();
    }

    private int GenerateUserId()
    {
        // Generate a unique user ID (this is a simple example, in a real-world scenario, you might use a more robust approach)
        return _users.Max(u => u.Id) + 1;
    }
}

