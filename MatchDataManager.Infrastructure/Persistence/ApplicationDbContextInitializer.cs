using MatchDataManager.Domain.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MatchDataManager.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApplicationDbContextInitializer> _logger;

    public ApplicationDbContextInitializer(
        ILogger<ApplicationDbContextInitializer> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitializeAsync(IConfiguration configuration)
    {
        try
        {
            CreateDbFolder(configuration);

            if (_context.Database.IsSqlite())
                await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorMessages.SharedExceptions.DatabaseInitializeErrorMessage);
            throw;
        }
    }

    private static void CreateDbFolder(IConfiguration configuration)
    {
        var dirPath = string.Concat(
            Environment.CurrentDirectory,
            Path.DirectorySeparatorChar,
            configuration["dbPath"]);

        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);
    }
}