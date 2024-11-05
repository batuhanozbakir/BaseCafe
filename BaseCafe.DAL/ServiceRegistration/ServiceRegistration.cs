using BaseCafe.DAL.Configurations;
using BaseCafe.DAL.Context;
using BaseCafe.DAL.Repositories;
using BaseCafe.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCafe.DAL.ServiceRegistration
{
    public static class ServiceRegistration
    {
        
        public static void AddDalService(this IServiceCollection services)
        {
            string conn = Configuration.ConnectionString;

            services.AddDbContext<MyDbContext>(opt => opt.UseSqlServer(conn));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
