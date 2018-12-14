using EventStore.ClientAPI;

namespace EventStore
{
    public static class StreamEventsSliceExtensions
    {
        public static void ThrowExceptionIfNotSuccessfulRead(this StreamEventsSlice streamEventsSlice)
        {
            switch (streamEventsSlice.Status)
            {
                case SliceReadStatus.StreamDeleted:
                    throw new StreamDeletedException(streamEventsSlice.Stream);
                case SliceReadStatus.StreamNotFound:
                    throw new StreamNotFoundException(streamEventsSlice.Stream);
            }
        }
    }
}