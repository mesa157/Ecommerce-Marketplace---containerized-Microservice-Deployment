using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TagRepository : ITagRepository
{
    private readonly ProductContext _context;

    public TagRepository(ProductContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tag>> GetAllTags()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task<Tag> GetTagById(int id)
    {
        return await _context.Tags.FindAsync(id);
    }

    public async Task AddTag(Tag tag)
    {
        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTag(Tag tag)
    {
        _context.Tags.Update(tag);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTag(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag != null)
        {
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }
    }
}