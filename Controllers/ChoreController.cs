using HouseRules.Data;
using HouseRules.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseRules.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChoreController : ControllerBase
{
    private HouseRulesDbContext _dbContext;

    public ChoreController(HouseRulesDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    // [Authorize]
    public IActionResult Get()
    {
        var chores = _dbContext.Chores
            .Select(chore => new
            {
                chore.Id,
                chore.Name,
                chore.Difficulty,
                chore.ChoreFrequencyDays
            })
            .ToList();

        return Ok(chores);
    }

    [HttpGet("{id}")]
    // [Authorize]
    public IActionResult GetChoreById(int id)
    {
        var chore = _dbContext.Chores
            .Include(c => c.ChoreAssignments)
                .ThenInclude(ca => ca.UserProfile)
                    .ThenInclude(up => up.IdentityUser)
            .Include(c => c.ChoreCompletions)
            .SingleOrDefault(c => c.Id == id);

        if (chore == null)
        {
            return NotFound($"Chore with ID {id} not found.");
        }

        var choreDTO = new ChoreDTO
        {
            Id = chore.Id,
            Name = chore.Name,
            Difficulty = chore.Difficulty,
            ChoreFrequencyDays = chore.ChoreFrequencyDays,
            ChoreAssignments = chore.ChoreAssignments
                .Select(ca => new ChoreAssignmentDTO
                {
                    Id = ca.Id,
                    UserProfileId = ca.UserProfileId,
                    UserName = ca.UserProfile.IdentityUser.UserName,
                    ChoreId = ca.ChoreId
                })
                .ToList(),
            ChoreCompletions = chore.ChoreCompletions
                .Select(cc => new ChoreCompletionDTO
                {
                    Id = cc.Id,
                    ChoreId = cc.ChoreId,
                    UserProfileId = cc.UserProfileId,
                    CompletedOn = cc.CompletedOn
                })
                .ToList()
        };

        return Ok(choreDTO);
    }

    [HttpPost("{id}/complete")]
    // [Authorize]

    public IActionResult CompleteChore(int id, [FromQuery] int userId)
    {
        var chore = _dbContext.Chores.SingleOrDefault(c => c.Id == id);
        if (chore == null)
        {
            return NotFound($"Chore with ID {id} not found.");
        }

        var user = _dbContext.UserProfiles.SingleOrDefault(u => u.Id == userId);
        if (user == null)
        {
            return NotFound($"User with ID {userId} not found.");
        }

        var choreCompletion = new ChoreCompletion
        {
            ChoreId = id,
            UserProfileId = userId,
            CompletedOn = DateTime.UtcNow
        };

        _dbContext.ChoreCompletions.Add(choreCompletion);
        _dbContext.SaveChanges();

        return NoContent();
    }


    [HttpPost]
    // [Authorize]
    public IActionResult CreateChore(ChoreCreateDTO choreDto)
    {
        // Validate the input
        if (string.IsNullOrWhiteSpace(choreDto.Name))
        {
            return BadRequest("Chore name is required.");
        }

        if (choreDto.Difficulty < 1 || choreDto.Difficulty > 5)
        {
            return BadRequest("Difficulty must be between 1 and 5.");
        }

        if (choreDto.ChoreFrequencyDays <= 0)
        {
            return BadRequest("Chore frequency must be greater than zero.");
        }

        // Create the new chore entity
        var newChore = new Chore
        {
            Name = choreDto.Name,
            Difficulty = choreDto.Difficulty,
            ChoreFrequencyDays = choreDto.ChoreFrequencyDays
        };

        // Add to database
        _dbContext.Chores.Add(newChore);
        _dbContext.SaveChanges();

        // Return 201 Created with the new chore's ID
        return CreatedAtAction(nameof(GetChoreById), new { id = newChore.Id }, newChore);
    }

    [HttpPut]
    // [Authorize]
    public IActionResult UpdateChore(int id, ChoreUpdateDTO choreDto)
    {
        if (choreDto == null)
        {
            return BadRequest("Invalid input.");
        }

        if (string.IsNullOrWhiteSpace(choreDto.Name))
        {
            return BadRequest("Chore Name is required.");
        }

        if (choreDto.Difficulty < 1 || choreDto.Difficulty > 5)
        {
            return BadRequest("Difficulty must be between 1 and 5");
        }

        if (choreDto.ChoreFrequencyDays <= 0)
        {
            return BadRequest("Chore frequency must be greater then zero.");
        }

        var chore = _dbContext.Chores.SingleOrDefault(c => c.Id == id);
        if (chore == null)
        {
            return NotFound($"Chore withID {id} NotFiniteNumberException found.");
        }

        chore.Name = choreDto.Name;
        chore.Difficulty = choreDto.Difficulty;
        chore.ChoreFrequencyDays = choreDto.ChoreFrequencyDays;

        _dbContext.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    // [Authorize]
    public IActionResult DeleteChore(int id)
    {
        var chore = _dbContext.Chores.SingleOrDefault(c => c.Id == id);
        if (chore == null)
        {
            return NotFound($"Chore with ID {id} not found.");
        }

        _dbContext.Chores.Remove(chore);
        _dbContext.SaveChanges();

        return NoContent();
    }

    [HttpPost("{id}/assign")]
    // [Authorize]
    public IActionResult AssignChore(int id, int userId)
    {
        var chore = _dbContext.Chores.SingleOrDefault(c => c.Id == id);
        if (chore == null)
        {
            return NotFound($"Chore with ID {id} not found.");
        }

        var user = _dbContext.UserProfiles.SingleOrDefault(u => u.Id == userId);
        if (user == null)
        {
            return NotFound($"User with ID {userId} not found.");
        }

        var existingAssignment = _dbContext.ChoreAssignments
            .SingleOrDefault(ca => ca.ChoreId == id && ca.UserProfileId == userId);
        if (existingAssignment != null)
        {
            return BadRequest("User is already assigned to this chore.");
        }

        var choreAssignment = new ChoreAssignment
        {
            ChoreId = id,
            UserProfileId = userId
        };

        _dbContext.ChoreAssignments.Add(choreAssignment);
        _dbContext.SaveChanges();

        return NoContent();
    }

    [HttpPost("{id}/unassign")]
    // [Authorize]
    public IActionResult UnassignChore(int id, int userId)
    {
        var chore = _dbContext.Chores.SingleOrDefault(c => c.Id == id);
        if (chore == null)
        {
            return NotFound($"Chore with ID {id} not found.");
        }

        var user = _dbContext.UserProfiles.SingleOrDefault(u => u.Id == userId);
        if (user == null)
        {
            return NotFound($"User with ID {userId} not found.");
        }

        var existingAssignment = _dbContext.ChoreAssignments
            .SingleOrDefault(ca => ca.ChoreId == id && ca.UserProfileId == userId);
        if (existingAssignment == null)
        {
            return NotFound($"User with ID {userId} is not assigned to chore with ID {id}.");
        }

        _dbContext.ChoreAssignments.Remove(existingAssignment);
        _dbContext.SaveChanges();

        return NoContent();
    }


}

