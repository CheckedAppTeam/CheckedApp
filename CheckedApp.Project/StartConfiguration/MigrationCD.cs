using CheckedAppProject.DATA.CheckedAppDbContext;
using Microsoft.EntityFrameworkCore;

namespace CheckedAppProject.API.StartConfiguration;

public class MigrationCD
{
    private readonly UserItemContext _userItemContext;

    public MigrationCD(UserItemContext userItemContext)
    {
        _userItemContext = userItemContext;
    }

    public void MigrationCheck()
    {
        if (_userItemContext.Database.CanConnect())
        {
           var pendingMigrations = _userItemContext.Database.GetPendingMigrations();
            if(pendingMigrations != null && pendingMigrations.Any())
            {
                _userItemContext.Database.Migrate();
            }
        }
    }
}
