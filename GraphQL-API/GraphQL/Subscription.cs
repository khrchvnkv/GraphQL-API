using GraphQL_API.Models;

namespace GraphQL_API.GraphQL
{
    public class Subscription
    { 
        [Subscribe]
        [Topic]
        public Platform OnPlatformAdded([EventMessage] Platform platform)
        {
            return platform; 
        }
        
        [Subscribe]
        [Topic]
        public Command OnCommandAdded([EventMessage] Command command)
        {
            return command; 
        }
    }
}