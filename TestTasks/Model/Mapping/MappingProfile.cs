using AutoMapper;
using TestTasks.Model;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Data, DataView>();
    }
}