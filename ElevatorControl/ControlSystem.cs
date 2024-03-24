using System.Collections.Concurrent;

namespace ElevatorControl
{
    public class ControlSystem
    {
        private Elevator _elevator;
        private ConcurrentQueue<Request> _requestQueue;

        public ControlSystem(Elevator elevator)
        {
            _elevator = elevator;
            _requestQueue = new ConcurrentQueue<Request>();
        }

        public async Task AddRequestAsync(Request request)
        {
            if (IsValidRequest(request))
            {
                _requestQueue.Enqueue(request);
                if (!_elevator.IsMoving)
                {
                    await ProcessRequestsAsync();
                }
            }
            else
            {
                Console.WriteLine($"Invalid request from floor {request.OriginFloor}.");
            }
        }

        private bool IsValidRequest(Request request)
        {
            switch (request.Type)
            {
                case RequestType.Call:
                    // Call requests are valid only if from the 1st or 5th floors
                    return request.OriginFloor == 1 || request.OriginFloor == 5;
                case RequestType.Up:
                    // Up requests are valid only from floors 2-4
                    return request.OriginFloor >= 2 && request.OriginFloor <= 4;
                case RequestType.Down:
                    // Down requests are valid only from floors 2-4
                    return request.OriginFloor >= 2 && request.OriginFloor <= 4;
                case RequestType.Inside:
                    // Inside requests are always valid, assuming the button pressed corresponds to a real floor
                    return request.DestinationFloor >= 1 && request.DestinationFloor <= 5;
                default:
                    return false;
            }
        }

        private List<Request> SortRequests(List<Request> requests, int currentFloor, Direction currentDirection)
        {
            // Separate requests into those that are in the direction of travel and those that are not
            var inDirectionRequests = new List<Request>();
            var oppositeDirectionRequests = new List<Request>();

            foreach (var request in requests)
            {
                if ((currentDirection == Direction.Up && request.DestinationFloor >= currentFloor) ||
                    (currentDirection == Direction.Down && request.DestinationFloor <= currentFloor))
                {
                    inDirectionRequests.Add(request);
                }
                else
                {
                    oppositeDirectionRequests.Add(request);
                }
            }

            // Sort inDirectionRequests by proximity to the current floor
            inDirectionRequests.Sort((a, b) => Math.Abs(a.DestinationFloor - currentFloor).CompareTo(Math.Abs(b.DestinationFloor - currentFloor)));

            // Sort oppositeDirectionRequests by proximity to the current floor, but they'll be processed later
            oppositeDirectionRequests.Sort((a, b) => Math.Abs(a.DestinationFloor - currentFloor).CompareTo(Math.Abs(b.DestinationFloor - currentFloor)));

            // Return a new list with in-direction requests first, followed by opposite direction requests
            return inDirectionRequests.Concat(oppositeDirectionRequests).ToList();
        }


        private async Task ProcessRequestsAsync()
        {
            if (_requestQueue.IsEmpty) return;

            var requests = _requestQueue.ToList();
            _requestQueue.Clear();

            var sortedRequests = SortRequests(requests, _elevator.CurrentFloor, _elevator.Direction);

            foreach (var request in sortedRequests)
            {
                await _elevator.MoveToFloorAsync(request.DestinationFloor);
            }
        }
    }

}
