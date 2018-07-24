using AutoMapper;
using FWS.Generic.Framework.Interfaces;

namespace FWS.Generic.Framework.Helpers
{
    public static class ITracksTimeHelper
    {
        public static void SetForCreated(this ITracksTime tracksTime)
        {
            tracksTime.Created = Clock.Now();
            tracksTime.Modified = Clock.Now();
        }

        public static void SetForModified(this ITracksTime tracksTime)
        {
            tracksTime.Modified = Clock.Now();
        }

        public static IMappingExpression<TSource, TDestination> IgnoreTimeStamps<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression) where TDestination : ITracksTime
        {
            return expression
                .ForMember(x => x.Created, o => o.Ignore())
                .ForMember(x => x.Modified, o => o.Ignore());
        }
    }
}
