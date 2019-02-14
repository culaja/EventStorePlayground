using System;
using Autofac;
using AutofacApplicationWrapup;
using Services;

namespace SimpleQueryApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<MainRegistrator>();
            var container = builder.Build();

            var initializer = container.Resolve<QuerySideInitializer>();
            initializer.Initialize();

            Console.ReadLine();
        }
    }
}