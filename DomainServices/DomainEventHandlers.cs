using System;
using BallEvents;
using Common;
using Common.Messaging;
using static Common.Result;

namespace DomainServices
{
    public static class DomainEventHandlers
    {
        public static Func<IDomainEvent, Result> DomainEventHandlersWith() =>
            e =>
            {
                switch (e)
                {
                    case BallPassed ballPassed:
                        return Ok();
                    case BallCreated ballCreated:
                        return Ok();
                    default:
                        return Ok();
                }
            };
    }
}