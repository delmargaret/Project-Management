using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class Map<T, N> where T : class where N : class
    {

        private IMapper mapper;

        public Map()
        {
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<T, N>()).CreateMapper();

        }

        public List<N> ListMap(IEnumerable<T> projects)
        {
            return mapper.Map<IEnumerable<T>, List<N>>(projects);
        }

        public N ObjectMap(T project)
        {
            return mapper.Map<T, N>(project);
        }

    }
}
