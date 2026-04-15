using PlatformService.Models;

namespace PlatformService.Data;


public class PlatformRepo : IPlatformRepo
{
    public PlatformRepo(AppDbContext context)
    {
        _context = context;
    }

    private readonly AppDbContext _context;

    public void CreatePlatform(Platform platform)
    {
        _context.Platforms.Add(platform);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _context.Platforms.ToList();
    }

    public Platform? GetPlatformById(int Id)
    {
        return _context.Platforms.FirstOrDefault(p => p.Id == Id);
    }

    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }
}