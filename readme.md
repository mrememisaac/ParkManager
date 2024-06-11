#Vehicle Parking System
This is a simple vehicle parking system that allows users to park their vehicles in a parking lot. 
The system is designed to be used by a parking lot manager who can add parking lots, park vehicles, and remove vehicles from the parking lot.

##ParkManager.Domain

This project contains the domain model for the vehicle parking system. The domain model consists of the following entities:

### Arrival
Represents an arrival event in the park management domain

### ArrivalDepartureBase
The ArrivalDepartureBase is a base class (also known as a superclass) used to create a general class that defines characteristics and behaviors that can be shared among its subclasses.
In this case, ArrivalDepartureBase contains properties and methods common to both arrival and departure events in the park management domain. This could include properties like EventTime, Location, Vehicle, etc., and methods that handle common operations related to both arrivals and departures.
The Arrival class inherits from ArrivalDepartureBase, meaning it automatically includes all the non-private properties and methods defined in ArrivalDepartureBase. This allows for code reuse and a logical, hierarchical organization of classes.


##ParkManager.Application

##ParkManager.Persistence

##ParkManager.Api

##ParkManager.Web

##ParkManager.UnitTests