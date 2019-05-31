using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain;
using Domain.Commands;
using Ports;
using static Domain.Ball;

namespace DomainServices
{
    internal static class BallCommandExecutors
    {
        public static Func<ICommand, Task<Result>> BallCommandExecutorsWith(IRepository repository) =>
            c =>
            {
                switch (c)
                {
                    case CreateBall createBall:
                        return CreateBallExecutorWith(repository)(createBall);
                    case PassBall passBall:
                        return PassBallExecutorWith(repository)(passBall);
                    default:
                        throw new NotSupportedException($"Command '{c}' can't be handled by {nameof(Ball)} aggregate.");
                }
            };
        
        private static Func<CreateBall, Task<Result>> CreateBallExecutorWith(IRepository repository) => 
            c => repository.InsertNew(NewBallWith(c.BallId, c.Size));

        private static Func<PassBall, Task<Result>> PassBallExecutorWith(IRepository repository) =>
            c => repository.BorrowBy<Ball>(c.BallId, ball => ball.PassTo(c.Destination));
    }
}