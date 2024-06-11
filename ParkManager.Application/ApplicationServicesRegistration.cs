using Microsoft.Extensions.DependencyInjection;
using ParkManager.Application.Features.Arrivals.Commands.AddArrival;
using ParkManager.Application.Features.Arrivals.Commands.UpdateArrival;
using ParkManager.Application.Features.Departures.Commands.AddDeparture;
using ParkManager.Application.Features.Departures.Commands.UpdateDeparture;
using ParkManager.Application.Features.Drivers.Commands.AddDriver;
using ParkManager.Application.Features.Drivers.Commands.UpdateDriver;
using ParkManager.Application.Features.Lanes.Commands.AddLane;
using ParkManager.Application.Features.Lanes.Commands.UpdateLane;
using ParkManager.Application.Features.Occasions.Commands.AddOccasion;
using ParkManager.Application.Features.Parks.Commands.UpdatePark;
using ParkManager.Application.Features.Slots.Commands.AddSlot;
using ParkManager.Application.Features.Tags.Commands.AddTag;
using ParkManager.Application.Features.Occasions.Commands.UpdateOccasion;
using ParkManager.Application.Features.Parks.Commands.AddPark;
using ParkManager.Application.Features.Slots.Commands.UpdateSlot;
using ParkManager.Application.Features.Tags.Commands.UpdateTag;
using ParkManager.Application.Features.Vehicles.Commands.AddVehicle;
using ParkManager.Application.Features.Vehicles.Commands.UpdateVehicle;
using System.Reflection;

namespace ParkManager.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<AddArrivalCommandValidator>();
            services.AddSingleton<UpdateArrivalCommandValidator>();
            services.AddSingleton<AddDepartureCommandValidator>();
            services.AddSingleton<UpdateDepartureCommandValidator>();
            services.AddSingleton<AddDriverCommandValidator>();
            services.AddSingleton<UpdateDriverCommandValidator>();
            services.AddSingleton<AddLaneCommandValidator>();
            services.AddSingleton<UpdateLaneCommandValidator>();
            services.AddSingleton<AddOccasionCommandValidator>();
            services.AddSingleton<UpdateOccasionCommandValidator>();            
            services.AddSingleton<AddParkCommandValidator>();
            services.AddSingleton<UpdateParkCommandValidator>();
            services.AddSingleton<AddSlotCommandValidator>();
            services.AddSingleton<UpdateSlotCommandValidator>();
            services.AddSingleton<AddTagCommandValidator>();
            services.AddSingleton<UpdateTagCommandValidator>();
            services.AddSingleton<AddVehicleCommandValidator>();
            services.AddSingleton<UpdateVehicleCommandValidator>();
            return services;
        }
    }
}
