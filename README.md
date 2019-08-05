# EventStorePlayground

EventStore Web Client: http://localhost:2113/web/index.html#/
User name: admin
Password: changeit

Enable projections:
mac: eventstore --run-projections=all --start-standard-projections=true
win: EventStore.ClusterNode.exe --run-projections=all --start-standard-projections=true

Enable in-memory database:
mac: sudo eventstore --run-projections=all --start-standard-projections=true --mem-db=true