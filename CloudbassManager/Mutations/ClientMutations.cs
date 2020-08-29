using Cloudbass.DataAccess.Repositories;
using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database.Models;
using Cloudbass.Types.Clients;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CloudbassManager.Mutations
{
    [ExtendObjectType(Name = "Mutation")]

    //CREATE
    public class ClientMutations
    {
        public async Task<CreateClientPayload> AddClientAsync(
            [Service] IClientRepository clientRepository,
            [Service] ITopicEventSender eventSender,
            CreateClientInput input,
            CancellationToken cancellationToken)
        {
            var addedClient = new Client
            {
                Name = input.Name,
                Email = input.Email,
                Address = input.Address,
                Tel = input.Tel,
                ToContact = input.ToContact

            };

            await clientRepository.CreateClientAsync(addedClient, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(addedClient, cancellationToken).ConfigureAwait(false);

            return new CreateClientPayload(addedClient);
        }


        //UPDATE
        public async Task<UpdateClientPayload> UpdateClientAsync(
            UpdateClientInput input, Guid id,
           [Service] ITopicEventSender eventSender,
           [Service] IClientRepository clientRepository,
           CancellationToken cancellationToken)
        {

            var clientToUpdate = await clientRepository.GetClientByIdAsync(id);



            if (clientToUpdate == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("matching client id not found in database.")
                        .SetCode("CLIENT_NOT_FOUND")
                        .Build());
            }

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                clientToUpdate.Name = input.Name;
            }

            if (!string.IsNullOrWhiteSpace(input.Email))
            {
                clientToUpdate.Email = input.Email;
            }

            if (!string.IsNullOrWhiteSpace(input.Tel))
            {
                clientToUpdate.Tel = input.Tel;
            }


            if (!string.IsNullOrWhiteSpace(input.Address))
            {
                clientToUpdate.Address = input.Address;
            }

            if (!string.IsNullOrWhiteSpace(input.ToContact))
            {
                clientToUpdate.ToContact = input.ToContact;
            }



            await clientRepository.UpdateClientAsync(clientToUpdate, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(clientToUpdate, cancellationToken).ConfigureAwait(false);

            return new UpdateClientPayload(clientToUpdate);

        }

        //delete

        public async Task<Client> DeleteClientAsync(
           [Service] IClientRepository clientRepository,
           [Service] ITopicEventSender eventSender,
           DeleteClientInput input, CancellationToken cancellationToken)
        {
            var clientToDelete = await clientRepository.GetClientByIdAsync(input.Id);
            await clientRepository.DeleteClientAsync(clientToDelete, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(clientToDelete, cancellationToken).ConfigureAwait(false);

            return clientToDelete;

        }

    }
}
