# DagbokReader
The dagbok reader is a hobby project whith the goal to create apps and desktop prgrams to work with the dagbok at vässarö scout center

## DataModel
The data model is used to be able to send data between different units needing information about the dagbok. The DataDescription.xml describes the binary data for a row in the dagbok.

### DataModel Types
- **Time** A 16 bit number that describes the time of day in minutes since midnight
- **Destination** An 8 bit id for a destination, Destinatinos are stored in a separate table
- **Number** An 8 bit number
- **Boat** A 4 bit boat Id referencing separate boat table
- **Crew** An 8 bit crew Id referencing a separate crew table
- **String** An 8 bit signed Id referencing two tables a staic table containing string that won't change that often and a dynamic table that is created when a dagbok is serialized
