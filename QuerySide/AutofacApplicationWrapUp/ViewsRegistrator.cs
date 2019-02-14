using Autofac;
using StudentViews;

namespace AutofacApplicationWrapup
{
    public sealed class ViewsRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentsPerCityView>().SingleInstance();
        }
    }
}