using Vendo.Application.Abstractions.Repositories;
using Vendo.Domain.Entities;
using Vendo.Persistence.Contexts;
using Vendo.Persistence.Implementations.Repostories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendo.Persistence.Implementations.Repostories
{
    internal class ColorRepository:Repository<Color>,IColorRepository
    {
        public ColorRepository(AppDbContext context):base(context) { }
    }
}
