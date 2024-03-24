using ElevatorControl;

var elevator = new Elevator();
var controlSystem = new ControlSystem(elevator);

// Simulating button presses
await controlSystem.AddRequestAsync(new Request { OriginFloor = 1, DestinationFloor = 5, Type = RequestType.Call }); // Call from 1st floor
await controlSystem.AddRequestAsync(new Request { OriginFloor = 4, DestinationFloor = 2, Type = RequestType.Down }); // Down from 4th floor
await controlSystem.AddRequestAsync(new Request { OriginFloor = 0, DestinationFloor = 3, Type = RequestType.Inside }); // Inside request to 3rd floor
await controlSystem.AddRequestAsync(new Request { OriginFloor = 2, DestinationFloor = 5, Type = RequestType.Up }); // Up from 2nd floor

Console.WriteLine("Elevator operations completed.");
