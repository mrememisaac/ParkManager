using AutoMapper;
using MediatR;
using ParkManager.Application.Features.Drivers.Commands.AddDriver;
using ParkManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkManager.Application.Features.Drivers.Queries.GetDriver
{
    public class GetDriverQuery : IRequest<GetDriverQueryResponse>
    {
        public int Id { get; set; }

        public GetDriverQuery(int id)
        {
            Id = id;
        }
    }
}
