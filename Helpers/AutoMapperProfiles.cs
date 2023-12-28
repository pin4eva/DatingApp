using Api.Extensions;
using Api.photos.Models;
using Api.users.DTOs;
using Api.users.Models;
using AutoMapper;

namespace Api.Helpers;

public class AutoMapperProfiles : Profile
{
  public AutoMapperProfiles()
  {
    CreateMap<User, MemberDTO>()
    .ForMember(dest =>
    dest.PhotoUrl,
    opt => opt.MapFrom(source =>
    source.Photos.FirstOrDefault(photo => photo.IsMain).Url
  )
  ).ForMember((dest) =>
dest.Age,
opt => opt.MapFrom(source => source.DateOfBirth.CalculateAge())
);
    CreateMap<Photo, PhotoDTO>();
    CreateMap<UpdateMemberDTO, User>();
  }

}
