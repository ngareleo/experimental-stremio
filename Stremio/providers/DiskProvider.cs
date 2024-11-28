namespace Stremio.Providers;

class DiskProvider
{
    private readonly ILogger _logger;

    public DiskProvider(ILogger<DiskProvider> logger)
    {
        _logger = logger;
    }

    public void OnStartup()
    {
        _logger.LogInformation("Attempting to read media from disk");
    }
}
