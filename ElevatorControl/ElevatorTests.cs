using NUnit.Framework;

namespace ElevatorControl
{
    [TestFixture]
    public class ElevatorTests
    {
        [Test]
        public async Task Elevator_Moves_To_Requested_Floor_Async()
        {
            var elevator = new Elevator();
            var controlSystem = new ControlSystem(elevator);
            await controlSystem.AddRequestAsync(new Request { DestinationFloor = 3 });

            Assert.Equals(3, elevator.CurrentFloor);
        }
    }
}
