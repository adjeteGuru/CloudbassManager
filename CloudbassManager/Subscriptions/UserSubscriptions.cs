using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace CloudbassManager.Subscriptions
{
    [ExtendObjectType(Name = "Subscription")]
    public class UserSubscriptions
    {
        [SubscribeAndResolve]
        public async Task<IAsyncEnumerable<User>> OnCreateUser(
           [Service]ITopicEventReceiver eventReceiver) =>
           await eventReceiver.SubscribeAsync<string, User>("CreateUser");
    }
}
