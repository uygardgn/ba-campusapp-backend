using AutoMapper;
using BACampusApp.Dtos.HomeWork;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
	public class HomeWorkProfile : Profile
	{
		public HomeWorkProfile()
		{
			
			CreateMap<HomeWorkListDto, HomeWork>().ReverseMap();
			CreateMap<HomeWork, HomeWorkDto>().ReverseMap();
			CreateMap<HomeWork,HomeWorkCreateDto>().ReverseMap();
			CreateMap<HomeWork,HomeWorkUpdateDto>().ReverseMap();
			CreateMap<HomeWork, HomeWorkDeletedListDto>();
			CreateMap<HomeWork, HomeWorkListByTrainerDto>();
			CreateMap<HomeWork, HomeworkListByStudentDto>();


        }

    }
	
}
