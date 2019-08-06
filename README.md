# EventStorePlayground

EventStore Web Client: http://localhost:2113/web/index.html#/
User name: admin
Password: changeit

Enable projections:
mac: eventstore --run-projections=all --start-standard-projections=true
win: EventStore.ClusterNode.exe --run-projections=all --start-standard-projections=true

Enable in-memory database:
mac: sudo eventstore --run-projections=all --start-standard-projections=true --mem-db=true
win: EventStore.ClusterNode.exe --run-projections=all --start-standard-projections=true --mem-db=true

Projection for aggregating all events of an aggregate type into one stream:
fromAll().when(
    {$any:function(s,e) 
        {
            if (e.metadata !== null && e.metadata.AggregateType !== null)
            {
                linkTo("aggtype-" + e.metadata.AggregateType, e)
            }
        }
    })