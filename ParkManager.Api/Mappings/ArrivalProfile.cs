using AutoMapper;
using ParkManager.Api.Models;
using ParkManager.Application.Features.Arrivals.Commands.AddArrival;
using ParkManager.Application.Features.Arrivals.Commands.UpdateArrival;
using ParkManager.Application.Features.Arrivals.Queries.GetArrival;

namespace ParkManager.Api.Mappings
{
    public class ArrivalProfile : Profile
    {
        public ArrivalProfile()
        {
            CreateMap<AddArrivalCommand, Arrival>().ReverseMap(); 
            CreateMap<UpdateArrivalCommand, Arrival>().ReverseMap();
            CreateMap<UpdateArrivalCommandResponse, Arrival>().ReverseMap();
            CreateMap<AddArrivalCommandResponse, Arrival>().ReverseMap();
            CreateMap<Arrival, GetArrivalQueryResponse>().ReverseMap();

        }
    }
}
