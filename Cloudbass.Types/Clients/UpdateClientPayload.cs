using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbass.Types.Clients
{
    public class UpdateClientPayload
    {
        public UpdateClientPayload(Client client)
        {
            Client = client;
        }

        public Client Client { get; set; }
    }
}
