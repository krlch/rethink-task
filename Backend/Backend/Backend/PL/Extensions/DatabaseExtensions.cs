using Backend.DAL;
using Backend.PL.Settings.AppSettings;

namespace Backend.PL.Extensions
{
    public static class DatabaseExtensions
    {
        public static void RegistryDatabase(this IServiceCollection services, AppSettings appSettings)
        {
            DBContext.PathToFile = appSettings.Database.ConnectionString;
        }
    }
}
