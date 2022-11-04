using Backend.BL.Services.Classes;
using Backend.BL.Services.Interfaces;
using Backend.DAL.Repositories.Classes;
using Backend.DAL.Repositories.Interfaces;

namespace Backend.PL.Extensions
{
    public static class InjectExtensions
    {
        public static void InjectDependencies(this IServiceCollection services)
        {
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContactsService, ContactsService>();
        }
    }
}
