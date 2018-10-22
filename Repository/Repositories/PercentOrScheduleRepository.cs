using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
using DAL.DataContext;
using Repository.Interfaces;
using Exeption;

namespace Repository.Repositories
{
    public class PercentOrScheduleRepository : IPercentOrScheduleRepository
    {
        private ManagementContext db;

        public PercentOrScheduleRepository(ManagementContext context)
        {
            this.db = context;
        }

        public PercentOrSchedule GetTypeById(int id)
        {
            if (db.PercentOrSchedules.Find(id) == null)
            {
                throw new NotFoundException();
            }
            return db.PercentOrSchedules.Find(id);
        }
    }
}
