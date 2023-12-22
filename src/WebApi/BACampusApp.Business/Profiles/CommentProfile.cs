using AutoMapper;
using BACampusApp.Dtos.Admin;
using BACampusApp.Dtos.Comment;
using BACampusApp.Dtos.Educations;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<Comment, CommentCreateDto>();
            CreateMap<CommentUpdateDto, Comment>();
            CreateMap<Comment, CommentUpdateDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Comment, CommentListDto>();
            CreateMap<Comment, CommentDeletedListDto>();

        }

    }
}
