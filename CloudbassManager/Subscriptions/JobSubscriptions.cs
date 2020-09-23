using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbassManager.Subscriptions
{
    [ExtendObjectType(Name = "Subscription")]
    public class JobSubscriptions
    {
        [SubscribeAndResolve]
        public async Task<IAsyncEnumerable<Job>> OnCreateJob(
          [Service]ITopicEventReceiver eventReceiver) =>
          await eventReceiver.SubscribeAsync<string, Job>("CreateJob");
    }
}
