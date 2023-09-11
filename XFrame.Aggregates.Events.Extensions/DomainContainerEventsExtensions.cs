﻿using System.Reflection;
using XFrame.Aggregates.Events.AggregateEvents;
using XFrame.DomainContainers;

namespace XFrame.Aggregates.Events.Extensions
{
    public static class DomainContainerEventsExtensions
    {
        public static IDomainContainer AddEvents(
            this IDomainContainer domainContainer,
            Assembly fromAssembly,
            Predicate<Type> predicate = null)
        {
            predicate = predicate ?? (t => true);
            var aggregateEventTypes = fromAssembly
                .GetTypes()
                .Where(t => !t.GetTypeInfo().IsAbstract && typeof(IAggregateEvent).GetTypeInfo().IsAssignableFrom(t))
                .Where(t => predicate(t));
            return domainContainer.AddTypes(aggregateEventTypes);
        }

        public static IDomainContainer AddEvents(
            this IDomainContainer domainContainer,
            params Type[] aggregateEventTypes)
        {
            return domainContainer.AddTypes(aggregateEventTypes);
        }
    }
}
