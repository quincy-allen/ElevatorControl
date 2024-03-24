namespace ElevatorControl
{
    public class Request
    {
        public int OriginFloor { get; set; }
        public int DestinationFloor { get; set; }
        public RequestType Type { get; set; }
    }
    public enum RequestType
    {
        Call, // For the first and fifth floors
        Up,   // For calling the elevator to go up from the 2nd, 3rd, or 4th floors
        Down, // For calling the elevator to go down from the 2nd, 3rd, or 4th floors
        Inside // For requests made from inside the elevator
    }
}
