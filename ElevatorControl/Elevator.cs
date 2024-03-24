namespace ElevatorControl
{
    public class Elevator
    {
        public int CurrentFloor { get; private set; }
        public bool IsMoving { get; private set; }
        public Direction Direction { get; private set; }

        public Elevator()
        {
            CurrentFloor = 1; 
            IsMoving = false;
            Direction = Direction.None;
        }

        public async Task MoveToFloorAsync(int floor)
        {
            Direction = floor > CurrentFloor ? Direction.Up : Direction.Down;
            IsMoving = true;

            int floorDifference = Math.Abs(floor - CurrentFloor);
            for (int i = 0; i < floorDifference; i++)
            {
                await Task.Delay(1000);
                CurrentFloor += (Direction == Direction.Up) ? 1 : -1;
                Console.WriteLine($"Elevator moving. Now at floor: {CurrentFloor}");
            }

            IsMoving = false;
            Direction = Direction.None;
        }
    }

    public enum Direction
    {
        Up,
        Down,
        None
    }

}
