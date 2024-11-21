using Microsoft.AspNetCore.Mvc;
using ProductService.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagRepository _tagRepository;

    public TagController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
    {
        var tags = await _tagRepository.GetAllTags();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tag>> GetTag(int id)
    {
        var tag = await _tagRepository.GetTagById(id);
        if (tag == null)
        {
            return NotFound();
        }
        return Ok(tag);
    }

    [HttpPost]
    public async Task<ActionResult<Tag>> AddTag(Tag tag)
    {
        await _tagRepository.AddTag(tag);
        return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag(int id, Tag tag)
    {
        if (id != tag.Id)
        {
            return BadRequest();
        }
        await _tagRepository.UpdateTag(tag);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(int id)
    {
        await _tagRepository.DeleteTag(id);
        return NoContent();
    }
}